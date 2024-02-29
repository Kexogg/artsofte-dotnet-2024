using Dal.Users.Models;

namespace Dal.Users;

/// <summary>
/// Репозиторий для работы с пользователями
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Получить пользователей
    /// </summary>
    /// <returns>Список пользователей</returns>
    Task<List<UserDal>> GetUsers();

    /// <summary>
    /// Получить пользователя по идентификатору
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Пользователь</returns>
    Task<UserDal?> GetUser(Guid userId);

    /// <summary>
    /// Получить пользователя по логину
    /// </summary>
    /// <param name="login">Логин пользователя</param>
    /// <returns>Пользователь</returns>
    Task<UserDal?> GetUser(string login);


    /// <summary>
    /// Создать пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <returns>Идентификатор пользователя</returns>
    Task<Guid> CreateUser(UserDal user);

    /// <summary>
    /// Обновить пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    Task<UserDal> UpdateUser(UserDal user);

    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    Task DeleteUser(Guid userId);
}