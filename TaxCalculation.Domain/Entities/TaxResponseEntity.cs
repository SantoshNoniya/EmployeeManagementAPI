using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculation.Domain.Entities
{
    public class TaxResponseEntity
    {
        public int Tax { get; set; }
        public decimal NetSalary { get; set; }
        public decimal Salary { get; set; }
    }
}
