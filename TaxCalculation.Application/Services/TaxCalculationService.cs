using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Interface;
using TaxCalculation.Domain.InterfaceInfrastructure;

namespace TaxCalculation.Application.Services
{
    public class TaxCalculationService:ITaxCalculation
    {
        private readonly ITaxCalculationRepository _taxCalculationRepository;
        public TaxCalculationService(ITaxCalculationRepository taxCalculationRepository ) 
        {
          _taxCalculationRepository = taxCalculationRepository;
        }

         public async Task<List<TaxResponseDto>> AnaualSalaryAsync(decimal amount)
        {
            var dtos = await _taxCalculationRepository.AnaualSalaryAsync(amount);

            var result = dtos.Select(e => new TaxResponseDto
            {
                Tax = e.Tax,
                Salary = e.Salary,
                NetSalary = e.NetSalary

            }).ToList();
            return result;

        }
    }
}
