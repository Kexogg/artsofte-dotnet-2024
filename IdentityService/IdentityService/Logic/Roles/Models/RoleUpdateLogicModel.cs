using Core;

namespace Logic.Roles.Models;

public class RoleUpdateLogicModel
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required ICollection<PermissionsEnum> Permissions { get; init; }
}