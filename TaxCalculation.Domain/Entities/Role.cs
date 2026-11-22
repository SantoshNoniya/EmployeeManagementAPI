using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculation.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime CreatedDate { get; set; } 
        public string ModifiedBy { get; set; }  
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; }

        // Add-Migration 
        // Update-Database
        // Remove-Migration
        // Add-Migration AddingRoleTable -Context DBConnString -OutputDir MigrationDetails
        // Update-Database -Context DBConnString

    }
}
