using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Domain.Entities;

namespace TaxCalculation.Domain.InterfaceInfrastructure
{
    public interface IUpdateEmployeeRepository
    {
        Task<List<Employee>> UpdateEmployeeAsync(List<Employee> updateDto);
    }
}
