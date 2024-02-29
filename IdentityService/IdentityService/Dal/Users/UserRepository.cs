using System.Collections.Concurrent;
using Dal.Users.Models;

namespace Dal.Users;

public class UserRepository: IUserRepository
{
    private static readonly ConcurrentDictionary<Guid, UserDal> Store = new();

    public Task<List<UserDal>> GetUsers()
    {
        return Task.FromResult(Store.Values.ToList());
    }

    public Task<UserDal?> GetUser(Guid userId)
    {
        return Task.FromResult(Store.Values.FirstOrDefault(x => x.Id == userId));
    }

    public Task<UserDal?> GetUser(string login)
    {
        return Task.FromResult(Store.Values.FirstOrDefault(x => x.Username == login));
    }

    public Task<Guid> CreateUser(UserDal user)
    {
        if (Store.TryAdd(user.Id, user))
        {
            return Task.FromResult(user.Id);
        }
        throw new Exception();
    }

    public Task<UserDal> UpdateUser(UserDal user)
    {
        if (Store.TryUpdate(user.Id, user, Store[user.Id]))
        {
            return Task.FromResult(user);
        }
        throw new Exception();
    }

    public Task DeleteUser(Guid userId)
    {
        if (Store.TryRemove(userId, out _))
        {
            return Task.CompletedTask;
        }
        throw new Exception();
    }
}