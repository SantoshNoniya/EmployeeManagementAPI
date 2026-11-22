using Microsoft.EntityFrameworkCore;
using TaxCalculation.Domain.Entities;
using TaxCalculation.Domain.InterfaceInfrastructure;
using TaxCalculation.Infrasctructure.Persistence;

namespace TaxCalculation.Infrasctructure.Repository
{
    public class GetEmployeeRepository: IGetEmployeeRepository
    {
        private readonly DBConnString _connString;

        public GetEmployeeRepository(DBConnString connString)
        {
            _connString = connString;
        }

        public async Task<List<Employee>> employeesDetailsAsync()
        {

           return await _connString.Employees.ToListAsync();       
        }

    }
}
