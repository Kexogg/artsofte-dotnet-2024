using Logic.Roles;
using Logic.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Logic;

public static class LogicStartUp
{
    public static IServiceCollection TryAddLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IUserLogicManager, UserLogicManager>();
        serviceCollection.TryAddScoped<IRoleLogicManager, RoleLogicManager>();
        return serviceCollection;
    }
}

