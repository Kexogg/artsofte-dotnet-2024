using Dal.Roles;
using Logic.Roles.Models;

namespace Logic.Roles;

public class RoleLogicManager(IRoleRepository roleRepository) : IRoleLogicManager
{
    public async Task<List<RoleDal>> GetRoles()
    {
        return await roleRepository.GetRoles();
    }

    public async Task<RoleDal> GetRole(Guid roleId)
    {
        var role = await roleRepository.GetRole(roleId);
        if (role != null)
        {
            return role;
        }
        throw new Exception();
    }

    public async Task<Guid> CreateRole(RoleCreateLogicModel logic)
    {
        var newRole = new RoleDal
        {
            Id = Guid.NewGuid(),
            Name = logic.Name,
            Description = logic.Description,
            Permissions = logic.Permissions
        };
        
        return await roleRepository.CreateRole(newRole);
    }

    public async Task<RoleDal> UpdateRole(RoleUpdateLogicModel logic)
    {
        var newRole = new RoleDal
        {
            Id = logic.Id,
            Name = logic.Name,
            Description = logic.Description,
            Permissions = logic.Permissions
        };
        return await roleRepository.UpdateRole(newRole);
    }
    
    public async Task DeleteRole(Guid roleId)
    {
        await roleRepository.DeleteRole(roleId);
    }
}