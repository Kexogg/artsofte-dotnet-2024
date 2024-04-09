using Core.RabbitLogic.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace Core.RabbitLogic;

public static class RabbitServiceStartup
{
    public static void AddRabbitServices(this IServiceCollection services)
    {
        services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
        services.AddSingleton<IPooledObjectPolicy<IModel>, PooledObjectPolicy>();
        services.AddSingleton<IRabbitPublisher, RabbitPublisher>();
    }
}