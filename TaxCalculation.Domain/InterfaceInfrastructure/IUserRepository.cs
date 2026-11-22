using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Domain.Entities;

namespace TaxCalculation.Domain.InterfaceInfrastructure
{
    public interface IUserRepository
    {
        Task<bool> CreateUserAsync(User userRepo);
        Task<User> GetUserByNameAsync(string name);
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetUserAllAsync();
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
    }
}
