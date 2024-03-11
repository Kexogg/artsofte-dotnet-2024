using Dal.Roles;
using Dal.Users;
using Logic.Users.Models;

namespace Logic.Users;

public class UserLogicManager(IUserRepository userRepository, IRoleRepository roleRepository) : IUserLogicManager
{
    /// <inheritdoc />
    public async Task<List<UserDal>> GetUsersAsync()
    {
        return await userRepository.GetUsers();
    }

    /// <inheritdoc />
    public async Task<UserDal> GetUserAsync(Guid userId)
    {
        var user = await userRepository.GetUserAsync(userId);
        if (user != null)
        {
            return user;
        }

        throw new Exception();
    }

    /// <inheritdoc />
    public async Task<Guid> CreateUserAsync(CreateUserModel user)
    {
        if (user.Roles == null || user.Roles.Count == 0)
        {
            //если валидацию можно делать только в бизнес-логике, то как отдать HTTP-ошибку?
            throw new Exception("Пользователь должен иметь хотя бы одну роль");
        }

        var roles = new List<RoleDal>(user.Roles.Select(role => roleRepository.GetRoleAsync(role).Result)!);
        return await userRepository.CreateUserAsync(
            new UserDal
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Username = user.Username,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password,
                ProfilePicture = user.ProfilePicture,
                Roles = roles
            }
        );
    }

    /// <inheritdoc />
    public async Task<UserDal> UpdateUserAsync(UserModel user)
    {
        var roles = new List<RoleDal>(user.Roles.Select(role => roleRepository.GetRoleAsync(role).Result)!);
        return await userRepository.UpdateUserAsync(
            new UserDal
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password,
                ProfilePicture = user.ProfilePicture,
                Roles = roles
            }
        );
    }

    /// <inheritdoc />
    public async Task DeleteUserAsync(Guid userId)
    {
        await userRepository.DeleteUserAsync(userId);
    }

    /// <inheritdoc />
    public async Task<UserDal[]> SearchUsersAsync(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            throw new Exception("Пустой запрос");
        }

        return await userRepository.SearchUsersAsync(query);
    }
}