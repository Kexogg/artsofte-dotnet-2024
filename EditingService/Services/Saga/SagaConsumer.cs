using DataExchange.Identity;
using MassTransit;
using Services.Saga.Models;

namespace Services.Saga;

public class SagaConsumer(IIdentityDataService identityDataService) : IConsumer<UserInfoRequest>
{
    public async Task Consume(ConsumeContext<UserInfoRequest> context)
    {
        try
        {
            await identityDataService.GetUserByIdAsync(context.Message.UserId, "id");
            await context.Publish<UserInfoResponse>(new
                { userId = context.Message.UserId, userInfo = context.Message.UserInfo });
        }
        catch (Exception exception)
        {
            await context.Publish<UserInfoFail>(new
            {
                userId = context.Message.UserId,
                userInfo = context.Message.UserInfo,
                Reason = exception.Message
            });
        }
    }
}