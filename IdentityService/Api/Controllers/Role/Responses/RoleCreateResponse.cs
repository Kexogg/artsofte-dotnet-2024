namespace Api.Controllers.Role.Responses;

public record RoleCreateResponse
{
    public required Guid Id { get; init; }
}