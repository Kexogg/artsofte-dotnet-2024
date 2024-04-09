using System.Text;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core.RabbitLogic.Services;

public class RabbitPublisher(IPooledObjectPolicy<IModel> objectPolicy, IOptions<RabbitConfig> options)
    : IRabbitPublisher
{
    private readonly DefaultObjectPool<IModel> _objectPool = new(objectPolicy, options.Value.MaxChannels);

    public void Publish<T>(PublishParameters<T> parameters, CancellationToken cancellationToken = default)
    {
        if (parameters.Data == null) return;
        var body = SerializeData(parameters.Data);
        using var channel = new PooledObject<IModel>(_objectPool);
        var properties = channel.Item.CreateBasicProperties();
        properties.Persistent = true;
        properties.ReplyTo = parameters.ReplyQueue;
        properties.CorrelationId = parameters.CorrelationId;
        channel.Item.BasicPublish(parameters.Exchange, parameters.RouteKey, properties, body);
    }

    public T Request<T>(RequestParameters parameters, CancellationToken cancellationToken = default)
    {
        var output = default(T);
        using var pooledObject = new PooledObject<IModel>(_objectPool);
        var channel = pooledObject.Item;

        var properties = channel.CreateBasicProperties();
        properties.CorrelationId = Guid.NewGuid().ToString();
        properties.ReplyTo = parameters.ReplyQueueName;
        var body = SerializeData(parameters.Data);

        channel.BasicPublish(exchange: string.Empty,
            routingKey: parameters.ServiceName,
            basicProperties: properties,
            body: body);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (_, eventArgs) =>
        {
            var received = eventArgs.BasicProperties;
            if (received.CorrelationId == properties.CorrelationId)
            {
                var response = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                output = JsonConvert.DeserializeObject<T>(response);
            }
            channel.BasicAck(eventArgs.DeliveryTag, false);
        };
        channel.BasicConsume(parameters.ReplyQueueName, false, consumer);
        while (output is null) Thread.Sleep(50);
        return output;
    }

    public void ExchangeDeclare(ExchangeDeclareParameters parameters, CancellationToken cancellationToken = default)
    {
        using var channel = new PooledObject<IModel>(_objectPool);
        channel.Item.ExchangeDeclare(parameters.Exchange, parameters.Type, parameters.Durable, parameters.AutoDelete,
            parameters.Arguments);
    }

    public QueueDeclareOk QueueDeclare(QueueDeclareParameters parameters, CancellationToken cancellationToken = default)
    {
        using var channel = new PooledObject<IModel>(_objectPool);
        return channel.Item.QueueDeclare(parameters.Queue, parameters.Durable, parameters.Exclusive,
            parameters.AutoDelete, parameters.Arguments);
    }

    private static byte[] SerializeData<T>(T data)
    {
        var jsonData = JsonConvert.SerializeObject(data);
        return Encoding.UTF8.GetBytes(jsonData);
    }
}