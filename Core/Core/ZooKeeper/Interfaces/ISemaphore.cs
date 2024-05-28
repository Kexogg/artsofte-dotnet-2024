namespace Core.ZooKeeper.Interfaces;

public interface ISemaphore
{
    Task<LockHandler?> AcquireAsync(TimeOut timeOut, CancellationToken cancellationToken = default);
    Task ReleaseAsync(string nodePath);
}