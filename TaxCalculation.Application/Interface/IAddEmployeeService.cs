using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;

namespace TaxCalculation.Application.Interface
{
    public interface IAddEmployeeService
    {
        Task<List<EmployeeDto>> AddEmployeeAsync(List<EmployeeDto> employee);
    }
}
