namespace Dal.Roles;

public interface IRoleRepository
{
    public Task<RoleDal> GetRole(Guid roleId);
    
    public Task<Guid> CreateRole(RoleDal role);
    
    public Task<RoleDal> UpdateRole(RoleDal role);
    
    public Task DeleteRole(Guid roleId);
    
    public Task<List<RoleDal>> GetRoles();
}