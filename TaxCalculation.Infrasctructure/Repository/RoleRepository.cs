using Microsoft.EntityFrameworkCore;
using TaxCalculation.Domain.Entities;
using TaxCalculation.Domain.InterfaceInfrastructure;
using TaxCalculation.Infrasctructure.Persistence;

namespace TaxCalculation.Infrasctructure.Repository
{
    public class RoleRepository: IRoleRepository
    {
      private readonly DBConnString _connString;
      public RoleRepository(DBConnString connString)
        {
            _connString = connString;
        }

      public async Task<Role> CreateRoleAsync(Role role)
        {           
            try
            {
                _connString.Roles.Add(role);
                await _connString.SaveChangesAsync();
                return role;
            }
            catch (Exception)
            {
                throw;
            }
        }

      public async  Task<List<Role>> GetAllRolesAsync()
      {
            return await _connString.Roles.ToListAsync();
      }
      public async Task<Role> GetRoleByIdAsync(int roleId)
      {
            return await _connString.Roles.AsNoTracking().FirstOrDefaultAsync(e=>e.Id==roleId);
      }
      public async Task<Role> GetRoleByNameAsync(string roleName)
      {
            return await _connString.Roles.AsNoTracking().FirstOrDefaultAsync(e => e.RoleName == roleName);
      }
      public async Task<Role> UpdateRoleAsync(Role role)
        {
            try
            {
                _connString.Roles.Update(role);
                await _connString.SaveChangesAsync();
                return role;
            }
            catch (Exception)
            {
                throw;
            }

        }
      public async Task<bool> DeleteRoleAsync(int id)
      {
            var role = await _connString.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (role == null)
                return false;

            _connString.Roles.Remove(role);

            await _connString.SaveChangesAsync();

            return true;
        }


    }
}
