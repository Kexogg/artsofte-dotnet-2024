namespace Api.Controllers.User.Requests;

public record UpdateUserRequest
{
    public required string Name { get; init; }
    public required string Username { get; init; }
    public required string PhoneNumber { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string ProfilePicture { get; init; }
    public required ICollection<Guid> Roles { get; init; }
}