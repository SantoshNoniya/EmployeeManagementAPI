using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Interface;

namespace TaxCalculation.API.Controllers;

//[Authorize]
[ApiController]
[Route("api/tax")]
public class TaxController : ControllerBase
{
    private readonly ITaxCalculation _taxCalculation;
    private readonly IGetEmployeeService _employeeService;
    private readonly IAddEmployeeService _addEmployeeService;
    private readonly IUpdateEmployeeService _updateEmployeeService;

    public TaxController(
        ITaxCalculation taxCalculation,
        IGetEmployeeService employeeService,
        IAddEmployeeService addEmployeeService,
        IUpdateEmployeeService updateEmployeeService)
    {
        _taxCalculation = taxCalculation;
        _employeeService = employeeService;
        _addEmployeeService = addEmployeeService;
        _updateEmployeeService = updateEmployeeService;
    }

    [HttpPost("calculate")]
    public async Task<ActionResult<List<TaxResponseDto>>> Calculate([FromQuery] decimal annualSalary)
    {
        if (annualSalary < 0)
            return BadRequest("Annual salary cannot be negative.");

        var result = await _taxCalculation.AnaualSalaryAsync(annualSalary);
        return Ok(result);
    }

    [HttpGet("employees")]
    public async Task<ActionResult<List<EmployeeDto>>> GetEmployees()
    {
        return Ok(await _employeeService.employeesDetailsAsync());
    }

    [HttpPost("employees")]
    public async Task<ActionResult<List<EmployeeDto>>> AddEmployees([FromBody] List<EmployeeDto> employees)
    {
        if (employees.Count == 0)
            return BadRequest("Employee list cannot be empty.");

        var result = await _addEmployeeService.AddEmployeeAsync(employees);
        return Ok(result);
    }

    [HttpPut("employees")]
    public async Task<ActionResult<List<EmployeeDto>>> UpdateEmployees([FromBody] List<EmployeeDto> employees)
    {
        if (employees.Count == 0)
            return BadRequest("Employee list cannot be empty.");

        var result = await _updateEmployeeService.UpdateEmployeeAsync(employees);
        return Ok(result);
    }
}
