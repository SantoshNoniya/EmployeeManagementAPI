using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Domain.Entities;

namespace TaxCalculation.Domain.InterfaceInfrastructure
{
    public interface IRolePrivilegeRepository
    {
        Task<List<RolePrivilege>> GetAllRolesPrivilegeAsync();
        Task<RolePrivilege> GetRolePrivilegeByIdAsync(int id);
        Task<RolePrivilege> GetRolePrivilegeByNameAsync(string roleName);
        Task<bool> CreateRolePrivilegeAsync(RolePrivilege RolePrivilege);
        Task<bool> UpdateRolePrivilegeAsync(RolePrivilege rolePrivilege);
        Task<bool> DeleteRolePrivilegeAsync(int id);
    }
}
