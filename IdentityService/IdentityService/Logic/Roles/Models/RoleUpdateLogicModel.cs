using Core.Dal.Entities;

namespace Logic.Roles.Models;

public class RoleUpdateLogicModel
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string Description { get; init; }
    public ICollection<PermissionsEnum> Permissions { get; init; }
}