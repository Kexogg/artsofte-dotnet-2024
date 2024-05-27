using DataExchange.Identity;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services.Interfaces;
using Services.Saga;

namespace Services;

public static class ServicesStartUp
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMassTransit(cfg =>
        {
            cfg.AddConsumer<SagaConsumer>();
            cfg.AddDelayedMessageScheduler();
            cfg.SetKebabCaseEndpointNameFormatter();
            cfg.SetInMemorySagaRepositoryProvider();
            cfg.UsingRabbitMq((brc, rbfc) =>
            {
                rbfc.UseInMemoryOutbox(brc);
                rbfc.UseDelayedMessageScheduler();
                rbfc.Host(Environment.GetEnvironmentVariable("RABBITMQ_HOSTNAME") ?? "localhost", h =>
                {
                    h.Username(Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "guest");
                    h.Password(Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest");
                });
                rbfc.ConfigureEndpoints(brc);
            });
        });
        serviceCollection.TryAddScoped<ICoursesService, CoursesService>();
        serviceCollection.TryAddScoped<IEventsService, EventsService>();
        serviceCollection.TryAddScoped<ISubjectsService, SubjectsService>();
        return serviceCollection;
    }
}