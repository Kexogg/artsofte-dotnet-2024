using DataExchange.Identity.Models;

namespace DataExchange.Identity;

public interface IIdentityDataService
{
    public Task<UserModel> GetUserByIdAsync(Guid userId, string queueName);
}