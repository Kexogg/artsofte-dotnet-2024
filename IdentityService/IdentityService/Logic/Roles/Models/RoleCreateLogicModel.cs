using Core.Dal.Entities;

namespace Logic.Roles.Models;

public class RoleCreateLogicModel
{
    public required string Name { get; init; }
    public string Description { get; init; }
    public ICollection<PermissionsEnum> Permissions { get; init; }
}