using Dal.Roles;
using Dal.Users;
using Logic.Users.Models;

namespace Logic.Users;

public class UserLogicManager(IUserRepository userRepository, IRoleRepository roleRepository) : IUserLogicManager
{
    /// <inheritdoc />
    public async Task<List<UserDal>> GetUsers()
    {
        return await userRepository.GetUsers();
    }
    /// <inheritdoc />
    public async Task<UserDal> GetUser(Guid userId)
    {
        var user = await userRepository.GetUser(userId);
        if (user != null)
        {
            return user;
        }

        throw new Exception();
    }
    /// <inheritdoc />
    public async Task<Guid> CreateUser(CreateUserModel user)
    {
        if (user.Roles == null || user.Roles.Count == 0)
        {
            //если валидацию можно делать только в бизнес-логике, то как отдать HTTP-ошибку?
            throw new Exception("Пользователь должен иметь хотя бы одну роль");
        }
        var roles = new List<RoleDal>(user.Roles.Select(role => roleRepository.GetRole(role).Result)!);
        return await userRepository.CreateUser(
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
    public async Task<UserDal> UpdateUser(UserModel user)
    {
        var roles = new List<RoleDal>(user.Roles.Select(role => roleRepository.GetRole(role).Result)!);
        return await userRepository.UpdateUser(
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
    public async Task DeleteUser(Guid userId)
    {
        await userRepository.DeleteUser(userId);
    }
}