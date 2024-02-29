using Core.Dal.Base;
using Dal.Roles;

namespace Dal.Users.Models;

/// <summary>
/// Модель пользователя в БД
/// </summary>
public record UserDal : BaseEntity<Guid>
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Name { get; init; }

    public required string Username { get; init; }

    public required string Password { get; init; }

    public required string PhoneNumber { get; init; }

    public required string Email { get; init; }

    public required string ProfilePicture { get; init; }

    public required ICollection<RoleDal> Roles { get; init; }
}