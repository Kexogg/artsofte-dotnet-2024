using Api.Controllers.Role.Responses;
using Api.Controllers.User.Requests;
using Api.Controllers.User.Responses;
using Logic.Users;
using Logic.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.User;

/// <summary>
/// Контроллер для работы с пользователями
/// </summary>
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserLogicManager _userLogicManager;

    // ReSharper disable once ConvertToPrimaryConstructor
    public UserController(IUserLogicManager userLogicManager)
    {
        _userLogicManager = userLogicManager;
    }

    [HttpGet]
    [ProducesResponseType<List<UserResponse>>(200)]
    public async Task<ActionResult> GetUsers()
    {
        var result = await _userLogicManager.GetUsers();
        return Ok(result.Select(user => new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Username = user.Username,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            ProfilePicture = user.ProfilePicture,
            Roles = user.Roles.Select(role => role.Id).ToList()
        }).ToList());
    }

    [HttpGet("search")]
    [ProducesResponseType<List<UserResponse>>(200)]
    public async Task<ActionResult> SearchUsers([FromQuery] string query)
    {
        var result = await _userLogicManager.SearchUsers(query);
        return Ok(result.Select(user => new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Username = user.Username,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            ProfilePicture = user.ProfilePicture,
            Roles = user.Roles.Select(role => role.Id).ToList()
        }).ToList());
    }


    [HttpPost]
    [ProducesResponseType<CreateUserResponse>(200)]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest dto)
    {
        var result = await _userLogicManager.CreateUser(new CreateUserModel
        {
            Name = dto.Name,
            Username = dto.Username,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Password = dto.Password,
            ProfilePicture = dto.ProfilePicture,
            Roles = dto.Roles
        });

        return Ok(new CreateUserResponse
        {
            Id = result
        });
    }

    [HttpPut("{userId:guid}")]
    [ProducesResponseType<UserResponse>(200)]
    public async Task<ActionResult> UpdateUser([FromBody] UpdateUserRequest dto, Guid userId)
    {
        var result = await _userLogicManager.UpdateUser(new UserModel
        {
            Id = userId,
            Name = dto.Name,
            Username = dto.Username,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Password = dto.Password,
            ProfilePicture = dto.ProfilePicture,
            Roles = dto.Roles
        });

        return Ok(new UserResponse
        {
            Id = result.Id,
            Name = result.Name,
            Username = result.Username,
            PhoneNumber = result.PhoneNumber,
            Email = result.Email,
            ProfilePicture = result.ProfilePicture,
            Roles = result.Roles.Select(role => role.Id).ToList()
        });
    }

    [HttpGet("{userId:guid}")]
    [ProducesResponseType<UserResponse>(200)]
    public async Task<ActionResult> GetUser(Guid userId)
    {
        var result = await _userLogicManager.GetUser(userId);
        return Ok(new UserResponseDetalied
        {
            Id = result.Id,
            Name = result.Name,
            Username = result.Username,
            PhoneNumber = result.PhoneNumber,
            Email = result.Email,
            ProfilePicture = result.ProfilePicture,
            Roles = result.Roles.Select(role =>
                new RoleInfoResponse
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    Permissions = role.Permissions
                }).ToList()
        });
    }

    [HttpDelete("{userId:guid}")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> DeleteUser(Guid userId)
    {
        await _userLogicManager.DeleteUser(userId);
        return Ok();
    }
}