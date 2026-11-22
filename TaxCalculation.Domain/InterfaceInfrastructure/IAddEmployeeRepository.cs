using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Domain.Entities;

namespace TaxCalculation.Domain.InterfaceInfrastructure
{
    public interface IAddEmployeeRepository
    {
        Task<List<Employee>> AddEmployeeAsync(List<Employee> employee);
    }
}
