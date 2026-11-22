using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculation.Application.DTOs
{
    public class CreateRoleDto
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
