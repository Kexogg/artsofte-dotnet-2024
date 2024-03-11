using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services.Interfaces;

namespace Services;

public static class ServicesStartUp
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<ICoursesService, CoursesService>();
        serviceCollection.TryAddScoped<IEventsService, EventsService>();
        serviceCollection.TryAddScoped<ISubjectsService, SubjectsService>();
        return serviceCollection;
    }
}