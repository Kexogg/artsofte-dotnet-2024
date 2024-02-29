namespace Api.Controllers.User.Requests;

/// <summary>
/// Запрос на создание пользователя
/// </summary>
public record CreateUserRequest
{
    public required string Name { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? ProfilePicture { get; init; }
    public required ICollection<Guid> Roles { get; init; }
}