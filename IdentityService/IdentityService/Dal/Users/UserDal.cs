using Core.Dal.Base;
using Dal.Roles;

namespace Dal.Users;

/// <summary>
/// Модель пользователя в БД
/// </summary>
public record UserDal : BaseEntity<Guid>
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public required string Username { get; init; }
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public required string Password { get; init; }
    /// <summary>
    /// Номер телефона пользователя
    /// </summary>
    public required string PhoneNumber { get; init; }
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public required string Email { get; init; }
    /// <summary>
    /// Ссылка на аватар пользователя
    /// </summary>
    public required string ProfilePicture { get; init; }
    /// <summary>
    /// Роли пользователя
    /// </summary>
    public required ICollection<RoleDal> Roles { get; init; }
}