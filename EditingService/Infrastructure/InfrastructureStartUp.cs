using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure;

public static class InfrastructureStartUp
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<ICoursesRepository, CoursesRepository>();
        serviceCollection.TryAddScoped<IEventsRepository, EventsRepository>();
        serviceCollection.TryAddScoped<ISubjectsRepository, SubjectsRepository>();
        return serviceCollection;
    }
}