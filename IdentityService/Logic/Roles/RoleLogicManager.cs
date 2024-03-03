using Dal.Roles;
using Logic.Roles.Models;

namespace Logic.Roles;

/// <inheritdoc />
public class RoleLogicManager(IRoleRepository roleRepository) : IRoleLogicManager
{
    /// <inheritdoc />
    public async Task<List<RoleDal>> GetRoles()
    {
        return await roleRepository.GetRoles();
    }

    /// <inheritdoc />
    public async Task<RoleDal> GetRole(Guid roleId)
    {
        return await roleRepository.GetRole(roleId);
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <inheritdoc />
    public async Task DeleteRole(Guid roleId)
    {
        await roleRepository.DeleteRole(roleId);
    }
}