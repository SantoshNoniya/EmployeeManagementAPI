using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Mapping;

namespace TaxCalculation.Application.Interface
{
    public interface IUserService
    {
        Task<ApiResponse<bool>> CreateUserAsync(UserDto userDto);
        Task<ApiResponse<ResponseUserDto>> GetUserByNameAsync(string name);
        Task<ApiResponse<ResponseUserDto>> GetUserByIdAsync(int id);
        Task<ApiResponse<List<ResponseUserDto>>> GetAllUserAsync();
        Task<ApiResponse<bool>> UpdateUserAsync(UserDto userDto);
        Task<ApiResponse<bool>> DeleteRoleAsync(int id);
        (string PasswordHash, string Salt) CreatePasswordHashWithSalt(string password);
       // (string PasswordHash, string Salt) CreatePasswordHash(string password);
        bool VerifyPassword(string password,string storedHash,string storedSalt);
    }
}
