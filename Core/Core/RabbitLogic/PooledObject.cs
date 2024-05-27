using Microsoft.Extensions.ObjectPool;

namespace Core.RabbitLogic;


public class PooledObject<T> : IDisposable where T : class
{
    private readonly DefaultObjectPool<T> _pool;
    public T Item { get; set; }

    public PooledObject(DefaultObjectPool<T> pool)
    {
        _pool = pool;
        Item = _pool.Get();
    }

    public void Dispose()
    {
        _pool.Return(this.Item);
    }
}