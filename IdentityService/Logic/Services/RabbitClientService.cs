using System.Text;
using Core.RabbitLogic;
using Core.RabbitLogic.Services;
using Dal.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Logic.Services;

public class RabbitClientService(
    IPooledObjectPolicy<IModel> objectPolicy,
    IOptions<RabbitConfig> options,
    IServiceProvider serviceProvider) : BackgroundService
{
    private readonly DefaultObjectPool<IModel> _objectPool = new(objectPolicy, options.Value.MaxChannels);

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channel = new PooledObject<IModel>(_objectPool).Item;
        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (_, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var props = eventArgs.BasicProperties;
            var message = Encoding.UTF8.GetString(body);
            using var serviceScope = serviceProvider
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            var userService = serviceScope.ServiceProvider.GetService(typeof(IUserRepository)) as IUserRepository;
            var user = userService?.GetUserAsync(new Guid(message.Substring(1, message.Length - 2))).Result;
            var rabbitPublisher = serviceProvider.GetRequiredService<IRabbitPublisher>();
            rabbitPublisher.Publish(new PublishParameters<UserDal>(user, string.Empty, props.ReplyTo, string.Empty,
                props.CorrelationId), stoppingToken);
            channel.BasicAck(eventArgs.DeliveryTag, false);
        };
        channel.BasicConsume("identity", false, consumer);
        return Task.CompletedTask;
    }
}