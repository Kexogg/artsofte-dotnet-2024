using System.Collections.Concurrent;

namespace Dal.Roles;

/// <inheritdoc />
public class RoleRepository : IRoleRepository
{
    private static readonly ConcurrentDictionary<Guid, RoleDal> Store = new();
    
    /// <inheritdoc />
    public async Task<RoleDal> GetRoleAsync(Guid roleId)
    {
        if (Store.TryGetValue(roleId, out var role))
        {
            return await Task.FromResult(role);
        }
        throw new Exception();
    }

    /// <inheritdoc />
    public async Task<Guid> CreateRoleAsync(RoleDal role)
    {
        if (Store.TryAdd(role.Id, role))
        {
            return await Task.FromResult(role.Id);
        }
        throw new Exception();
    }

    /// <inheritdoc />
    public async Task<RoleDal> UpdateRoleAsync(RoleDal role)
    {
        if (!Store.TryGetValue(role.Id, out var existingRole)) throw new Exception();
        Store.TryUpdate(role.Id, role, existingRole);
        return await Task.FromResult(role);
    }
    
    /// <inheritdoc />
    public async Task DeleteRoleAsync(Guid roleId)
    {
        if (!Store.TryRemove(roleId, out _)) throw new Exception();
    }
    
    /// <inheritdoc />
    public async Task<RoleDal[]> GetRolesAsync()
    {
        return await Task.FromResult(Store.Values.ToArray());
    }
}