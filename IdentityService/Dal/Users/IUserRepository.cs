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
    Task<UserDal?> GetUserAsync(Guid userId);

    /// <summary>
    /// Получить пользователя по логину
    /// </summary>
    /// <param name="login">Логин пользователя</param>
    /// <returns>Пользователь</returns>
    Task<UserDal?> GetUserAsync(string login);


    /// <summary>
    /// Создать пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <returns>Идентификатор пользователя</returns>
    Task<Guid> CreateUserAsync(UserDal user);

    /// <summary>
    /// Обновить пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    Task<UserDal> UpdateUserAsync(UserDal user);

    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    Task DeleteUserAsync(Guid userId);
    
    /// <summary>
    /// Поиск пользователей
    /// </summary>
    /// <param name="query">Строка запроса</param>
    /// <returns>Список пользователей</returns>
    Task<UserDal[]> SearchUsersAsync(string query);
}