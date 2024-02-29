namespace Logic.Users.Models;

/// <summary>
/// Модель пользователя для изменения
/// </summary>
public class UserModel
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string PhoneNumber { get; init; }
    public required string Email { get; init; }
    public required string ProfilePicture { get; init; }
    public required ICollection<Guid> Roles { get; init; }
}