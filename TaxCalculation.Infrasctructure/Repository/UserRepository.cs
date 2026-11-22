using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Domain.Entities;
using TaxCalculation.Domain.InterfaceInfrastructure;
using TaxCalculation.Infrasctructure.Persistence;

namespace TaxCalculation.Infrasctructure.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly DBConnString _connString;
        public UserRepository(DBConnString connString)
        {
            _connString = connString;
        }

        public async Task<bool> CreateUserAsync(User userRepo)
        {
            try
            {
                _connString.Users.Add(userRepo);
                var result=  await _connString.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUserByNameAsync(string name)
        {

           return await _connString.Users.FirstOrDefaultAsync(u=>u.UserName==name);
   
        }

        public async Task<User> GetUserByIdAsync(int id)
        {

            return await _connString.Users.FirstOrDefaultAsync(u => u.Id == id);

        }

        public async Task<List<User>> GetUserAllAsync()
        {

            return await _connString.Users.ToListAsync();

        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {

              var result=  await _connString.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                var user = await _connString.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (user == null)
                {
                    return false;
                }

                _connString.Users.Remove(user);

                var result = await _connString.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
