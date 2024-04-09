using Core.RabbitLogic.Services;
using DataExchange.Identity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataExchange.Identity;

public class IdentityDataService : IIdentityDataService
{
    private readonly IRabbitPublisher _rabbitPublisher;
    private readonly string _connectionType;

    public IdentityDataService(IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _rabbitPublisher = serviceProvider.GetRequiredService<IRabbitPublisher>();
        _connectionType = "rpc";
    }

    public Task<UserModel> GetUserByIdAsync(Guid userId, string queueName)
    {
        _ = _rabbitPublisher.QueueDeclare(new QueueDeclareParameters("identity", false, true, false, null));

        _ = _rabbitPublisher.QueueDeclare(new QueueDeclareParameters(queueName, false, true, false, null));

        var result = _rabbitPublisher.Request<UserModel>(new RequestParameters(userId.ToString(), "identity_service", "chat_service"));
        return Task.FromResult(result);
    }
}