using Dal.Roles;
using Logic.Roles.Models;

namespace Logic.Roles;

/// <summary>
/// Интерфейс для работы с ролями
/// </summary>
public interface IRoleLogicManager
{
    /// <summary>
    /// Получить роли
    /// </summary>
    /// <returns>Список ролей</returns>
    Task<RoleDal[]> GetRolesAsync();

    /// <summary>
    /// Получить роль по идентификатору
    /// </summary>
    /// <param name="roleId">GUID роли</param>
    /// <returns>Роль</returns>
    Task<RoleDal> GetRoleAsync(Guid roleId);

    /// <summary>
    /// Создать роль
    /// </summary>
    /// <param name="logic">Модель для создания роли</param>
    /// <returns>GUID роли</returns>
    Task<Guid> CreateRoleAsync(RoleCreateLogicModel logic);

    /// <summary>
    /// Обновить роль
    /// </summary>
    /// <param name="logic">Модель для обновления роли</param>
    /// <returns>Обновленная роль</returns>
    Task<RoleDal> UpdateRoleAsync(RoleUpdateLogicModel logic);

    /// <summary>
    /// Удалить роль
    /// </summary>
    /// <param name="roleId">GUID роли</param>
    /// <returns>Результат удаления</returns>
    Task DeleteRoleAsync(Guid roleId);
}