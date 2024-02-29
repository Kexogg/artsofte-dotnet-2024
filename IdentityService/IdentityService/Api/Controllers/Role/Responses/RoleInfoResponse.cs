using Core.Dal.Entities;

namespace Api.Controllers.Role.Responses;

public record RoleInfoResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required ICollection<PermissionsEnum> Permissions { get; init; }
};