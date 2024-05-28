using Core.ZooKeeper.Interfaces;
using org.apache.zookeeper;

namespace Core.ZooKeeper;

public abstract class ZooKeeperSemaphore(org.apache.zookeeper.ZooKeeper zooKeeper, string semaphorePath, int maxCount)
    : ISemaphore
{
    private static readonly IReadOnlyList<byte> AcquiredMarker = "ACQUIRED"u8.ToArray();

    public async Task<LockHandler?> AcquireAsync(TimeOut timeout, CancellationToken cancellationToken = default)
    {
        var timeoutSource = new CancellationTokenSource(timeout.TimeSpan);

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var nodePath = await zooKeeper.createAsync(semaphorePath + "/lock-", null, ZooDefs.Ids.OPEN_ACL_UNSAFE,
                        CreateMode.EPHEMERAL_SEQUENTIAL)
                    .ConfigureAwait(false);

                var children = await zooKeeper.getChildrenAsync(semaphorePath, false)
                    .ConfigureAwait(false);

                var sortedChildren = await ZooKeeperSequentialPath.FilterAndSortAsync(
                    parentNode: semaphorePath,
                    childrenNames: children.Children,
                    prefix: "semaphore-",
                    zooKeeper,
                    alternatePrefix: null
                ).ConfigureAwait(false);



                var state = new State(nodePath, sortedChildren);

                var currentNodeIndex = Array.FindIndex(state.SortedChildren, t => t.Path == state.EphemeralNodePath);

                if (currentNodeIndex < maxCount)
                {

                    await zooKeeper.setDataAsync(nodePath, AcquiredMarker.ToArray()).ConfigureAwait(false);

                    return new LockHandler(this, nodePath);
                }



                var waitCompletionSource = new TaskCompletionSource<bool>();
                await using var timeoutRegistration =
                    timeoutSource.Token.Register(s => ((TaskCompletionSource<bool>)s!).TrySetResult(false),
                        waitCompletionSource);
                await using var cancellationRegistration =
                    cancellationToken.Register(s => ((TaskCompletionSource<bool>)s!).TrySetCanceled(),
                        waitCompletionSource);

                var watcher = new WaitCompletionSource(waitCompletionSource);


                if (!waitCompletionSource.Task.IsCompleted
                    && await WaitAsync(zooKeeper, watcher, state))
                {
                    waitCompletionSource.TrySetResult(true);
                }

                if (!await waitCompletionSource.Task.ConfigureAwait(false))
                {
                    return null;
                }
            }
            finally
            {
                timeoutSource.Dispose();
            }
        }
    }


    private async Task<bool> WaitAsync(org.apache.zookeeper.ZooKeeper zK, Watcher watcher, State state)
    {
            var ephemeralNodeIndex = 
                Array.FindIndex(state.SortedChildren, t => t.Path == state.EphemeralNodePath);

            if (ephemeralNodeIndex == maxCount)
            {
                var childNames = new HashSet<string>((await zK.getChildrenAsync(semaphorePath, watcher).ConfigureAwait(false)).Children);
                return state.SortedChildren.Take(ephemeralNodeIndex)
                    .Any(t => !childNames.Contains(t.Path[(t.Path.LastIndexOf('/') + 1)..]));
            }

            var nextLowestChildData = 
                await zK.getDataAsync(state.SortedChildren[ephemeralNodeIndex - 1].Path, watcher).ConfigureAwait(false);

            return nextLowestChildData.Data.SequenceEqual(AcquiredMarker);
    }

    public async Task ReleaseAsync(string nodePath)
    {
        await zooKeeper.deleteAsync(nodePath).ConfigureAwait(false);
    }

    private record State(string EphemeralNodePath, (string Path, int SequenceNumber, string Prefix)[] SortedChildren);
}