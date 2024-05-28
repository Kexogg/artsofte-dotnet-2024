using System.Globalization;

namespace Core.ZooKeeper;

internal static class ZooKeeperSequentialPath
{
    public static async ValueTask<(string Path, int SequenceNumber, string Prefix)[]> FilterAndSortAsync(
        string parentNode,
        IEnumerable<string> childrenNames,
        string prefix,
        org.apache.zookeeper.ZooKeeper zooKeeper,
        string? alternatePrefix = null)
    {
        var ephemeralChildrenWithPrefix = GetEphemeralChildrenWithPrefix(childrenNames, parentNode, prefix, alternatePrefix);

        if (ephemeralChildrenWithPrefix.Count == 0)
        {
            return [];
        }

        ephemeralChildrenWithPrefix.Sort((a, b) => a.UnsignedSequenceNumber.CompareTo(b.UnsignedSequenceNumber));

        var (maxGap, maxGapEndIndex) = FindMaxGap(ephemeralChildrenWithPrefix);

        if (maxGap >= 4_000_000_000u)
        {
            return ReorderByLowestIndex(ephemeralChildrenWithPrefix, maxGapEndIndex);
        }

        var creationTimeTasksByChildPath =
            ephemeralChildrenWithPrefix.ToDictionary(t => t.Path, t => GetNodeCreationTimeAsync(t.Path, zooKeeper));
        await Task.WhenAll(creationTimeTasksByChildPath.Values).ConfigureAwait(false);

        ephemeralChildrenWithPrefix.RemoveAll(t =>
            creationTimeTasksByChildPath.TryGetValue(t.Path, out var creationTimeTask) && creationTimeTask.Result == null);

        if (ephemeralChildrenWithPrefix.Count == 0)
        {
            return [];
        }

        var oldestChild = ephemeralChildrenWithPrefix.Select((t, index) =>
                (creationTime: creationTimeTasksByChildPath[t.Path].Result!.Value, index))
            .OrderBy(t => t.creationTime)
            .ThenBy(t => t.index)
            .First();

        return ReorderByLowestIndex(ephemeralChildrenWithPrefix, oldestChild.index);
    }

    private static List<(string Path, uint UnsignedSequenceNumber, string Prefix)> GetEphemeralChildrenWithPrefix(
        IEnumerable<string> childrenNames, string parentNode, string prefix, string? alternatePrefix)
    {
        var result = new List<(string Path, uint UnsignedSequenceNumber, string Prefix)>();
        foreach (var childName in childrenNames)
        {
            int? childSequenceNumber;
            string? childPrefix;
            if (GetSequenceNumberOrDefault(childName, prefix) is { } prefixSequenceNumber)
            {
                childSequenceNumber = prefixSequenceNumber;
                childPrefix = prefix;
            }
            else if (alternatePrefix != null
                     && GetSequenceNumberOrDefault(childName, alternatePrefix) is { } alternatePrefixSequenceNumber)
            {
                childSequenceNumber = alternatePrefixSequenceNumber;
                childPrefix = alternatePrefix;
            }
            else
            {
                childSequenceNumber = null;
                childPrefix = null;
            }

            if (childPrefix != null)
            {
                result.Add(($"{parentNode.TrimEnd('/')}/{childName}", unchecked((uint)childSequenceNumber!.Value),
                    childPrefix));
            }
        }

        return result;
    }

    private static (uint maxGap, int maxGapEndIndex) FindMaxGap(List<(string Path, uint UnsignedSequenceNumber, string Prefix)> ephemeralChildrenWithPrefix)
    {
        uint maxGap = 0;
        int maxGapEndIndex = -1;
        for (var i = 0; i < ephemeralChildrenWithPrefix.Count; ++i)
        {
            var gapEndIndex = (i + 1) % ephemeralChildrenWithPrefix.Count;
            var gap = unchecked(ephemeralChildrenWithPrefix[gapEndIndex].UnsignedSequenceNumber -
                                ephemeralChildrenWithPrefix[i].UnsignedSequenceNumber);
            if (gap <= maxGap) continue;
            maxGap = gap;
            maxGapEndIndex = gapEndIndex;
        }

        return (maxGap, maxGapEndIndex);
    }

    private static (string Path, int SequenceNumber, string Prefix)[] ReorderByLowestIndex(
        List<(string Path, uint UnsignedSequenceNumber, string Prefix)> ephemeralChildrenWithPrefix, int lowestIndex)
    {
        var result = new (string Path, int SequenceNumber, string Prefix)[ephemeralChildrenWithPrefix.Count];
        for (var i = 0; i < result.Length; ++i)
        {
            var element = ephemeralChildrenWithPrefix[(i + lowestIndex) % result.Length];
            result[i] = (element.Path, unchecked((int)element.UnsignedSequenceNumber), element.Prefix);
        }

        return result;
    }

    private static int? GetSequenceNumberOrDefault(string pathOrName, string prefix)
    {
        var prefixStartIndex = pathOrName.LastIndexOf('/') + 1;
        if (pathOrName.IndexOf(prefix, prefixStartIndex, StringComparison.Ordinal) != prefixStartIndex)
        {
            return null;
        }

        var counterSuffix = pathOrName[(prefixStartIndex + prefix.Length)..];
        return (
                   (counterSuffix.Length == 10 && counterSuffix[0] != '+')
                   || (counterSuffix.Length == 11 && counterSuffix[0] == '-')
               )
               && int.TryParse(counterSuffix, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture,
                   out var sequenceNumber)
            ? sequenceNumber
            : default(int?);
    }

    private static async Task<long?> GetNodeCreationTimeAsync(string path, org.apache.zookeeper.ZooKeeper zooKeeper)
    {
        return (await zooKeeper.existsAsync(path).ConfigureAwait(false))?.getCtime();
    }
}