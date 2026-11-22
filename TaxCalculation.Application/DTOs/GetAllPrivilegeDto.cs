using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculation.Application.DTOs
{
    public class GetAllPrivilegeDto
    {
        public int Id { get; set; }
        public string RolePrivilegeName { get; set; }
        public string RoleDescription { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
