using DataExchange.Identity.Models;

namespace Services.Saga.Models;

public record UserInfoFail(Guid UserId, UserModel UserInfo)
{
    public UserInfoFail() : this(default, default) { }
}