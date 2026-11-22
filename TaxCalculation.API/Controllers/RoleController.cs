using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Interface;

namespace TaxCalculation.API.Controllers;

[Authorize]
[ApiController]
[Route("api/roles")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto roleDto)
    {
        var result = await _roleService.CreateRoleAsync(roleDto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var result = await _roleService.GetAllRolesAsync();
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRoleById(int id)
    {
        var result = await _roleService.GetRoleByIdAsync(id);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("by-name/{name}")]
    public async Task<IActionResult> GetRoleByName(string name)
    {
        var result = await _roleService.GetRoleByNameAsync(name);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleDto roleDto)
    {
        if (id != roleDto.Id)
            return BadRequest(new { message = "Route id and request id must match." });

        var result = await _roleService.UpdateRoleAsync(roleDto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var result = await _roleService.DeleteRoleAsync(id);
        return StatusCode(result.StatusCode, result);
    }
}
