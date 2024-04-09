using RabbitMQ.Client;

namespace Core.RabbitLogic.Services
{
    public abstract record PublishParameters<T>(T? Data, string Exchange, string RouteKey, string ReplyQueue, string CorrelationId);
    public abstract record RequestParameters(object Data, string ServiceName, string ReplyQueueName);
    public abstract record ExchangeDeclareParameters(string Exchange, string Type, bool Durable, bool AutoDelete, IDictionary<string, object> Arguments);
    public abstract record QueueDeclareParameters(string Queue, bool Durable, bool Exclusive, bool AutoDelete, IDictionary<string, object> Arguments);

    public interface IRabbitPublisher
    {
        void Publish<T>(PublishParameters<T> parameters, CancellationToken cancellationToken = default);
        T Request<T>(RequestParameters parameters, CancellationToken cancellationToken = default);
        void ExchangeDeclare(ExchangeDeclareParameters parameters, CancellationToken cancellationToken = default);
        QueueDeclareOk QueueDeclare(QueueDeclareParameters parameters, CancellationToken cancellationToken = default);
    }
}