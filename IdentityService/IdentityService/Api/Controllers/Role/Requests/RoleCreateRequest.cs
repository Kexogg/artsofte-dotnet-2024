using Core.Dal.Entities;

namespace Api.Controllers.Role.Requests;

public record RoleCreateRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required ICollection<PermissionsEnum> Permissions { get; init; }
};