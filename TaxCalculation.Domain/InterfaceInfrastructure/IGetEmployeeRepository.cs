using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Domain.Entities;

namespace TaxCalculation.Domain.InterfaceInfrastructure
{
    public interface IGetEmployeeRepository
    {
        Task<List<Employee>> employeesDetailsAsync();
    }
}
