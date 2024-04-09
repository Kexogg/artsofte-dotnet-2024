using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Core.RabbitLogic;

public class PooledObjectPolicy : IPooledObjectPolicy<IModel>
{
    private readonly RabbitConfig _config;
    private readonly IConnection _connection;
    
    public PooledObjectPolicy(IOptions<RabbitConfig> optionsAccs)
        : this(optionsAccs.Value)
    { }

    public PooledObjectPolicy(RabbitConfig config)
    {
        _config = config;
        _connection = GetConnection();
    }

    private IConnection GetConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = _config.HostName,
            UserName = _config.UserName,
            Password = _config.Password,
            VirtualHost = _config.VHost,
            Port = _config.Port,
        };
        return factory.CreateConnection();
    }
    

    public IModel Create()
    {
        return _connection.CreateModel();
    }

    public bool Return(IModel obj)
    {
        if (obj.IsOpen) return true;
        obj.Dispose();
        return false;

    }
}