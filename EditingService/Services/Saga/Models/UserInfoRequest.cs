using DataExchange.Identity.Models;

namespace Services.Saga.Models;

public record UserInfoRequest(Guid UserId, UserModel UserInfo)
{
    public UserInfoRequest() : this(default, default) { }
}