using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Interface;

namespace TaxCalculation.API.Controllers;

[Authorize]
[ApiController]
[Route("api/role-privileges")]
public class RolePrivilegeController : ControllerBase
{
    private readonly IRolePrivilegeService _rolePrivilegeService;

    public RolePrivilegeController(IRolePrivilegeService rolePrivilegeService)
    {
        _rolePrivilegeService = rolePrivilegeService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRolePrivilege([FromBody] RolePrivilegeRequestDto request)
    {
        var result = await _rolePrivilegeService.CreateRolePrivilegeAsync(request);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRolePrivileges()
    {
        var result = await _rolePrivilegeService.GetAllRolesPrivilegeAsync();
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRolePrivilegeById(int id)
    {
        var result = await _rolePrivilegeService.GetRolePrivilegeByIdAsync(id);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("by-name/{name}")]
    public async Task<IActionResult> GetRolePrivilegeByName(string name)
    {
        var result = await _rolePrivilegeService.GetRolePrivilegeByNameAsync(name);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRolePrivilege(int id, [FromBody] RolePrivilegeRequestDto request)
    {
        if (id != request.Id)
            return BadRequest(new { message = "Route id and request id must match." });

        var result = await _rolePrivilegeService.UpdateRolePrivilegeAsync(request);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRolePrivilege(int id)
    {
        var result = await _rolePrivilegeService.DeleteRolePrivilegeAsync(id);
        return StatusCode(result.StatusCode, result);
    }
}
