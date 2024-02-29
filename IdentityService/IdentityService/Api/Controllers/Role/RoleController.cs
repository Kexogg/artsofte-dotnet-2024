using Api.Controllers.Role.Requests;
using Api.Controllers.Role.Responses;
using Logic.Roles;
using Logic.Roles.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Role;

[Route("api/roles")]
public class RoleController(IRoleLogicManager roleLogicManager) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<List<RoleInfoResponse>>(200)]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await roleLogicManager.GetRoles();
        return Ok(roles.Select(x => new RoleInfoResponse
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Permissions = x.Permissions
        }).ToList());
    }
    
    [HttpGet("{roleId:guid}")]
    [ProducesResponseType<RoleInfoResponse>(200)]
    public async Task<IActionResult> GetRole(Guid roleId)
    {
        var role = await roleLogicManager.GetRole(roleId);
        return Ok(new RoleInfoResponse
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            Permissions = role.Permissions
        });
    }
    
    [HttpPost]
    [ProducesResponseType<RoleCreateResponse>(200)]
    public async Task<IActionResult> CreateRole([FromBody] RoleCreateRequest request)
    {
        var role = new RoleCreateLogicModel
        {
            Name = request.Name,
            Description = request.Description,
            Permissions = request.Permissions
        };
        var roleId = await roleLogicManager.CreateRole(role);
        return Ok(new RoleCreateResponse
        {
            Id = roleId
        });
    }
    [HttpPut("{roleId:guid}")]
    [ProducesResponseType<RoleInfoResponse>(200)]
    public async Task<IActionResult> UpdateRole([FromBody] RoleCreateRequest request, Guid roleId)
    {
        var role = new RoleUpdateLogicModel
        {
            Id = roleId,
            Name = request.Name,
            Description = request.Description,
            Permissions = request.Permissions
        };
        await roleLogicManager.UpdateRole(role);
        return Ok(new RoleInfoResponse
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            Permissions = role.Permissions
        });
    }
    [HttpDelete("{roleId:guid}")]
    public async Task<IActionResult> DeleteRole(Guid roleId)
    {
        await roleLogicManager.DeleteRole(roleId);
        return Ok();
    }
}