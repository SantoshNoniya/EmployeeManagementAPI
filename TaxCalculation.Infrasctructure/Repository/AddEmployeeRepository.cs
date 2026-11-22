using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Domain.Entities;
using TaxCalculation.Domain.InterfaceInfrastructure;
using TaxCalculation.Infrasctructure.Persistence;

namespace TaxCalculation.Infrasctructure.Repository
{
    public class AddEmployeeRepository : IAddEmployeeRepository
    {
        private readonly DBConnString _dBConnString;

        public AddEmployeeRepository(DBConnString dbConnString)
        {
            _dBConnString = dbConnString;
        }
        public async Task<List<Employee>> AddEmployeeAsync(List<Employee> employee)
        {
                         _dBConnString.Employees.AddRange(employee);
                        await _dBConnString.SaveChangesAsync();
            return employee;

        }
    }
}
