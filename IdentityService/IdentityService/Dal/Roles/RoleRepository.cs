using System.Collections.Concurrent;

namespace Dal.Roles;

public class RoleRepository : IRoleRepository
{
    private static readonly ConcurrentDictionary<Guid, RoleDal> Store = new();
    
    public Task<RoleDal> GetRole(Guid roleId)
    {
        var role = Store.Values.FirstOrDefault(x => x.Id == roleId);
        if (role != null)
        {
            return Task.FromResult(role);
        }
        throw new Exception();
    }

    public Task<Guid> CreateRole(RoleDal role)
    {
        if (Store.TryAdd(role.Id, role))
        {
            return Task.FromResult(role.Id);
        }
        throw new Exception();
    }

    public Task<RoleDal> UpdateRole(RoleDal role)
    {
        if (Store.TryGetValue(role.Id, out var existingRole))
        {
            Store.TryUpdate(role.Id, role, existingRole);
        }
        throw new Exception();
    }

    public Task DeleteRole(Guid roleId)
    {
        if (Store.TryRemove(roleId, out _))
        {
            return Task.CompletedTask;
        }
        throw new Exception();
    }

    public Task<List<RoleDal>> GetRoles()
    {
        return Task.FromResult(Store.Values.ToList());
    }
}