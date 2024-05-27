using DataExchange.Identity.Models;

namespace Services.Saga.Models;

public record UserInfoResponse(Guid UserId, UserModel UserInfo)
{
    public UserInfoResponse() : this(default, default) { }
}