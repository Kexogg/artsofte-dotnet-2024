using System.Collections.Concurrent;

namespace Dal.Users;

/// <inheritdoc />
public class UserRepository : IUserRepository
{
    private static readonly ConcurrentDictionary<Guid, UserDal> Store = new();

    /// <inheritdoc />
    public async Task<List<UserDal>> GetUsers()
    {
        return await Task.FromResult(Store.Values.ToList());
    }

    /// <inheritdoc />
    public async Task<UserDal?> GetUserAsync(Guid userId)
    {
        if (Store.TryGetValue(userId, out var user))
        {
            return await Task.FromResult(user);
        }

        throw new Exception();
    }

    /// <inheritdoc />
    public async Task<UserDal?> GetUserAsync(string login)
    {
        return await Task.FromResult(Store.Values.FirstOrDefault(x => x.Username == login));
    }

    /// <inheritdoc />
    public async Task<Guid> CreateUserAsync(UserDal user)
    {
        if (Store.TryAdd(user.Id, user))
        {
            return await Task.FromResult(user.Id);
        }

        throw new Exception();
    }

    /// <inheritdoc />
    public async Task<UserDal> UpdateUserAsync(UserDal user)
    {
        if (Store.TryUpdate(user.Id, user, Store[user.Id]))
        {
            return await Task.FromResult(user);
        }

        throw new Exception();
    }

    /// <inheritdoc />
    public async Task DeleteUserAsync(Guid userId)
    {
        if (!Store.TryRemove(userId, out _)) throw new Exception();
    }
    
    /// <inheritdoc />
    public async Task<UserDal[]> SearchUsersAsync(string query)
    {
        return await Task.FromResult(Store.Values.Where(x => x.Name.Contains(query)).ToArray());
    }
}