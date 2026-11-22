using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Domain.Entities;

namespace TaxCalculation.Application.Interface
{
    public interface IGetEmployeeService
    {
        Task<List<EmployeeDto>> employeesDetailsAsync();
    }
}
