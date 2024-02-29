using Dal.Users.Models;
using Logic.Users.Models;

namespace Logic.Users;

/// <summary>
/// Работа с пользователем
/// </summary>
public interface IUserLogicManager
{
    /// <summary>
    /// Получить пользователей
    /// </summary>
    /// <returns>Список пользователей</returns>
    Task<List<UserDal>> GetUsers();

    
    /// <summary>
    /// Получить пользователя по Guid
    /// </summary>
    Task<UserDal> GetUser(Guid userId);

    /// <summary>
    /// Создать пользователя 
    /// </summary>
    /// <param name="user">Пользователь</param>
    Task<Guid> CreateUser(CreateUserModel user);
    
    /// <summary>
    /// Обновить пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    Task<UserDal> UpdateUser(UserModel user);
    
    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <param name="userId">GUID пользователя</param>
    Task DeleteUser(Guid userId);
}