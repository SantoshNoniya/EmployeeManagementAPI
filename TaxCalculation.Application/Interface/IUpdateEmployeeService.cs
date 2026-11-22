using System;
using System.Threading;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;

namespace TaxCalculation.Application.Interface
{
    /// <summary>
    /// Defines operations to update employee records.
    /// Implementations should handle validation, persistence, and concurrency concerns.
    /// </summary>
    public interface IUpdateEmployeeService
    {
        Task<List<EmployeeDto>> UpdateEmployeeAsync(List<EmployeeDto>  updateDto);
    }
}
