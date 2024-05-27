using DataExchange.Identity.Models;

namespace Services.Saga.Models;

public record UserInfoSuccess(Guid UserId, UserModel UserInfo)
{
    public UserInfoSuccess() : this(default, default) { }
}