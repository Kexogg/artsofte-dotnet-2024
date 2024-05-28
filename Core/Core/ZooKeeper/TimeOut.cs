namespace Core.ZooKeeper;
public readonly struct TimeOut : IEquatable<TimeOut>, IComparable<TimeOut>
{
    private readonly int _inMilliseconds;

    private TimeOut(TimeSpan? timeout, string paramName = "timeout")
    {
        if (timeout is { } timeoutValue)
        {
            var totalMilliseconds = (long)timeoutValue.TotalMilliseconds;
            if (totalMilliseconds is < -1 or > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: paramName,
                    actualValue: timeoutValue,
                    message: $"{TimeSpan.FromMilliseconds(int.MaxValue)}{Environment.NewLine}"
                );
            }

            _inMilliseconds = (int)totalMilliseconds;
        }
        else
        {
            _inMilliseconds = Timeout.Infinite;
        }
    }

    private bool IsInfinite => _inMilliseconds == Timeout.Infinite;
    public TimeSpan TimeSpan => TimeSpan.FromMilliseconds(_inMilliseconds);
    public override string ToString() => IsInfinite ? "INF" : TimeSpan.ToString();

    public bool Equals(TimeOut that) => _inMilliseconds == that._inMilliseconds;
    public override bool Equals(object? obj) => obj is TimeOut that && Equals(that);
    public override int GetHashCode() => _inMilliseconds;
    public int CompareTo(TimeOut that) => _inMilliseconds.CompareTo(that._inMilliseconds);

    public static bool operator ==(TimeOut a, TimeOut b) => a.Equals(b);
    public static bool operator !=(TimeOut a, TimeOut b) => !(a == b);

    public static implicit operator TimeOut(TimeSpan? timeout) => new(timeout);
}