using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Mapping;

namespace TaxCalculation.Application.Interface
{
    public interface IRoleService
    {
        Task<ApiResponse<List<GetRoleDetailsDto>>> GetAllRolesAsync();
        Task<ApiResponse<GetRoleDetailsDto>> GetRoleByIdAsync(int id);
        Task<ApiResponse<GetRoleDetailsDto>> GetRoleByNameAsync(string name);
        Task<ApiResponse<CreateRoleDto>> CreateRoleAsync(CreateRoleDto roleDto);
        Task<ApiResponse<RoleDto>> UpdateRoleAsync(RoleDto roleDto);
        Task<ApiResponse<bool>> DeleteRoleAsync(int id);
    }
}
