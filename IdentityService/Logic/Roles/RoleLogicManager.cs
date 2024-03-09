using Dal.Roles;
using Logic.Roles.Models;

namespace Logic.Roles;

/// <inheritdoc />
public class RoleLogicManager(IRoleRepository roleRepository) : IRoleLogicManager
{
    /// <inheritdoc />
    public async Task<RoleDal[]> GetRolesAsync()
    {
        return await roleRepository.GetRolesAsync();
    }

    /// <inheritdoc />
    public async Task<RoleDal> GetRoleAsync(Guid roleId)
    {
        return await roleRepository.GetRoleAsync(roleId);
    }

    /// <inheritdoc />
    public async Task<Guid> CreateRoleAsync(RoleCreateLogicModel logic)
    {
        var newRole = new RoleDal
        {
            Id = Guid.NewGuid(),
            Name = logic.Name,
            Description = logic.Description,
            Permissions = logic.Permissions
        };

        return await roleRepository.CreateRoleAsync(newRole);
    }

    /// <inheritdoc />
    public async Task<RoleDal> UpdateRoleAsync(RoleUpdateLogicModel logic)
    {
        var newRole = new RoleDal
        {
            Id = logic.Id,
            Name = logic.Name,
            Description = logic.Description,
            Permissions = logic.Permissions
        };
        return await roleRepository.UpdateRoleAsync(newRole);
    }

    /// <inheritdoc />
    public async Task DeleteRoleAsync(Guid roleId)
    {
        await roleRepository.DeleteRoleAsync(roleId);
    }
}