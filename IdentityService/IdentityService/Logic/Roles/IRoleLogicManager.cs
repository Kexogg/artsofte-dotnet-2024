using Dal.Roles;
using Logic.Roles.Models;

namespace Logic.Roles;

public interface IRoleLogicManager
{
    Task<List<RoleDal>> GetRoles();
    Task<RoleDal> GetRole(Guid roleId);
    Task<Guid> CreateRole(RoleCreateLogicModel logic);
    Task<RoleDal> UpdateRole(RoleUpdateLogicModel logic);
    Task DeleteRole(Guid roleId);
}