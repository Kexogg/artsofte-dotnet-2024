using Core.Dal.Base;

namespace DataExchange.Identity.Models;

/// <summary>
/// Модель пользователя
/// </summary>
public record UserModel : BaseEntity<Guid>
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
    public required string? PhoneNumber { get; init; }

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
    public required ICollection<RoleModel> Roles { get; init; }
}