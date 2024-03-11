namespace Dal.Roles;

/// <summary>
/// Репозиторий для работы с ролями
/// </summary>
public interface IRoleRepository
{
    /// <summary>
    /// Получить роль по идентификатору
    /// </summary>
    /// <param name="roleId">GUID роли</param>
    /// <returns>Роль</returns>
    public Task<RoleDal> GetRoleAsync(Guid roleId);

    /// <summary>
    /// Создать роль
    /// </summary>
    /// <param name="role">Роль</param>
    /// <returns>GUID роли</returns>
    public Task<Guid> CreateRoleAsync(RoleDal role);

    /// <summary>
    /// Обновить роль
    /// </summary>
    /// <param name="role">Роль</param>
    /// <returns>Обновленная роль</returns>
    public Task<RoleDal> UpdateRoleAsync(RoleDal role);

    /// <summary>
    /// Удалить роль
    /// </summary>
    /// <param name="roleId">GUID роли</param>
    public Task DeleteRoleAsync(Guid roleId);

    /// <summary>
    /// Получить роли
    /// </summary>
    /// <returns>Список ролей</returns>
    public Task<RoleDal[]> GetRolesAsync();
}