using System.Collections.Concurrent;

namespace Dal.Users;

/// <inheritdoc />
public class UserRepository : IUserRepository
{
    private static readonly ConcurrentDictionary<Guid, UserDal> Store = new();

    /// <inheritdoc />
    public Task<List<UserDal>> GetUsers()
    {
        return Task.FromResult(Store.Values.ToList());
    }

    /// <inheritdoc />
    public Task<UserDal?> GetUser(Guid userId)
    {
        return Task.FromResult(Store.Values.FirstOrDefault(x => x.Id == userId));
    }

    /// <inheritdoc />
    public Task<UserDal?> GetUser(string login)
    {
        return Task.FromResult(Store.Values.FirstOrDefault(x => x.Username == login));
    }

    /// <inheritdoc />
    public Task<Guid> CreateUser(UserDal user)
    {
        if (Store.TryAdd(user.Id, user))
        {
            return Task.FromResult(user.Id);
        }

        throw new Exception();
    }

    /// <inheritdoc />
    public Task<UserDal> UpdateUser(UserDal user)
    {
        if (Store.TryUpdate(user.Id, user, Store[user.Id]))
        {
            return Task.FromResult(user);
        }

        throw new Exception();
    }

    /// <inheritdoc />
    public Task DeleteUser(Guid userId)
    {
        if (Store.TryRemove(userId, out _))
        {
            return Task.CompletedTask;
        }

        throw new Exception();
    }
    
    /// <inheritdoc />
    public Task<List<UserDal>> SearchUsers(string query)
    {
        return Task.FromResult(Store.Values.Where(x => x.Name.Contains(query)).ToList());
    }
}