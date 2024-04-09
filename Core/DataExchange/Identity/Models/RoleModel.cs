using Core;
using Core.Dal.Base;

namespace DataExchange.Identity.Models;

/// <summary>
/// Модель роли
/// </summary>
public record RoleModel : BaseEntity<Guid>
{
    /// <summary>
    /// Имя роли
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Описание роли
    /// </summary>
    public required string Description { get; init; }

    /// <summary>
    /// Права роли
    /// </summary>
    public required ICollection<PermissionsEnum> Permissions { get; init; }
}