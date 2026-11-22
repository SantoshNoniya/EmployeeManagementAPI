using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Domain.Entities;
using TaxCalculation.Domain.InterfaceInfrastructure;

namespace TaxCalculation.Infrasctructure.Repository
{
    public class TaxCalculationRepository : ITaxCalculationRepository
    {
        public async Task<List<TaxResponseEntity>> AnaualSalaryAsync(decimal annualSalary)
        {

            //int[] number = { 2, 4, 5, 5, 7, 4 };

            //var re = number
            //    .GroupBy(g => g)
            //    //.Where(c => c.Count() > 1)
            //    .Select(g => new
            //    {
            //        number = g.Key,
            //       // count = g.Count()
            //    }).ToList();

            //var re = number
            //            .GroupBy(x => x)
            //            .FirstOrDefault(g => g.Count() > 1)?
            //            .Key;

            //var res =     number
            //             .GroupBy(x => x)
            //             .Where(g => g.Count() == 1)
            //             .Select(g => g.Key)
            //             .ToList();

            //var res = number
            //         .GroupBy(x => x)
            //         .Where(g => g.Count() > 1)
            //         .SelectMany(g => g)
            //         .ToList();
           decimal tax = 0;

            if (annualSalary <= 400000)
            {
                tax = 0;
            }
            else if (annualSalary <= 800000)
            {
                tax = (annualSalary - 400000) * 0.05m;
            }
            else if (annualSalary <= 1200000)
            {
                tax = 20000 + (annualSalary - 800000) * 0.10m;
            }
            else if (annualSalary <= 1600000)
            {
                tax = 60000 + (annualSalary - 1200000) * 0.15m;
            }
            else if (annualSalary <= 2000000)
            {
                tax = 120000 + (annualSalary - 1600000) * 0.20m;
            }
            else if (annualSalary <= 2400000)
            {
                tax = 200000 + (annualSalary - 2000000) * 0.25m;
            }
            else
            {
                tax = 300000 + (annualSalary - 2400000) * 0.30m;
            }

            var result = new List<TaxResponseEntity>
           {
             new TaxResponseEntity
             {
                 Salary = annualSalary,
                 Tax = (int)tax,   // or change Tax to decimal
                 NetSalary = annualSalary-tax
             }
           };

            return await Task.FromResult(result);
        }
    }
}
