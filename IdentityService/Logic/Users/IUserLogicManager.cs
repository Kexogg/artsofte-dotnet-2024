using Dal.Users;
using Logic.Users.Models;

namespace Logic.Users;

/// <summary>
/// Логика работы с пользователями
/// </summary>
public interface IUserLogicManager
{
    /// <summary>
    /// Получить пользователей
    /// </summary>
    /// <returns>Список пользователей</returns>
    Task<List<UserDal>> GetUsersAsync();

    
    /// <summary>
    /// Получить пользователя по Guid
    /// </summary>
    /// <param name="userId">GUID пользователя</param>
    /// <returns>Пользователь</returns>
    Task<UserDal> GetUserAsync(Guid userId);

    /// <summary>
    /// Создать пользователя 
    /// </summary>
    /// <param name="user">Пользователь</param>
    Task<Guid> CreateUserAsync(CreateUserModel user);
    
    /// <summary>
    /// Обновить пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <returns>Обновленный пользователь</returns>
    Task<UserDal> UpdateUserAsync(UserModel user);
    
    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <param name="userId">GUID пользователя</param>
    Task DeleteUserAsync(Guid userId);

    /// <summary>
    /// Поиск пользователей
    /// </summary>
    /// <param name="query">Строка запроса</param>
    /// <returns>Список пользователей</returns>
    Task<UserDal[]> SearchUsersAsync(string query);
}