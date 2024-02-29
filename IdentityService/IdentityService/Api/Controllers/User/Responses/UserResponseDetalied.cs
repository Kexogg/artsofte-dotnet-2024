using Api.Controllers.Role.Responses;

namespace Api.Controllers.User.Responses;

public record UserResponseDetalied
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Username { get; init; }
    public required string? PhoneNumber { get; init; }
    public required string? Email { get; init; }
    public required string? ProfilePicture { get; init; }
    public required ICollection<RoleInfoResponse> Roles { get; init; }
}