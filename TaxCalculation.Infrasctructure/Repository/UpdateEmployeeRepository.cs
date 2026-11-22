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
    public class UpdateEmployeeRepository:IUpdateEmployeeRepository
    {
        private readonly DBConnString _connString;

        public UpdateEmployeeRepository(DBConnString connString)
        {
            _connString = connString;
        }

        public async Task<List<Employee>> UpdateEmployeeAsync(List<Employee> updateDto)
        {

                 _connString.UpdateRange(updateDto);
                await  _connString.SaveChangesAsync();
               return updateDto;

        }
    }
}
