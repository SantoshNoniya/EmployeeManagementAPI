using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;

namespace TaxCalculation.Application.Interface
{
    public interface ITaxCalculation
    {
        Task<List<TaxResponseDto>> AnaualSalaryAsync(decimal amount);
    }
}
