using Core;

namespace Logic.Roles.Models;

public class RoleCreateLogicModel
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required ICollection<PermissionsEnum> Permissions { get; init; }
}