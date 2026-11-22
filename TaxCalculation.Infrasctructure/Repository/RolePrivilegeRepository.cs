using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaxCalculation.Domain.Entities;
using TaxCalculation.Domain.InterfaceInfrastructure;
using TaxCalculation.Infrasctructure.Persistence;

namespace TaxCalculation.Infrasctructure.Repository
{
    public class RolePrivilegeRepository : IRolePrivilegeRepository
    {
        private readonly DBConnString _connString;
        public RolePrivilegeRepository(DBConnString dBConnString) 
        { 
            _connString = dBConnString;
        }
        public async Task<bool> CreateRolePrivilegeAsync(RolePrivilege rolePrivilege)
        {
          var result = await _connString.RolePrivileges.AddAsync(rolePrivilege);
            if (result != null)
                {
                    await _connString.SaveChangesAsync();
                    return true;
                }
                return false;
        }

        public async Task<bool> DeleteRolePrivilegeAsync(int id)
        {
           var dto= await _connString.RolePrivileges.FirstOrDefaultAsync(e=> e.Id == id && !e.IsDeleted);
            if (dto == null)
            {
              return false;
            }
            dto.IsDeleted = true;
            dto.IsActive = false;
            dto.ModifiedDate = DateTime.UtcNow;
            dto.ModifiedBy = dto.ModifiedBy;

            var affectedRows = await _connString.SaveChangesAsync();
            if (affectedRows > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<RolePrivilege>> GetAllRolesPrivilegeAsync()
        {
            return await _connString.RolePrivileges.ToListAsync();
        }

        public async Task<RolePrivilege> GetRolePrivilegeByIdAsync(int id)
        {
            return await _connString.RolePrivileges.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RolePrivilege> GetRolePrivilegeByNameAsync(string rolePrivilegeName)
        {

            return await _connString.RolePrivileges.AsNoTracking().FirstOrDefaultAsync(x => x.RolePrivilegeName == rolePrivilegeName);
        }

        public async Task<bool> UpdateRolePrivilegeAsync(RolePrivilege rolePrivilege)
        {
            if (rolePrivilege is null)
                return false;

            _connString.RolePrivileges.Update(rolePrivilege);

            var affectedRows = await _connString.SaveChangesAsync();

            return affectedRows > 0;
        }
    }
}
