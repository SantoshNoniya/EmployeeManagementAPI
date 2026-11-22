using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Mapping;

namespace TaxCalculation.Application.Interface
{
    public interface IRolePrivilegeService
    {
        Task<ApiResponse<List<GetAllPrivilegeDto>>> GetAllRolesPrivilegeAsync();
        Task<ApiResponse<GetAllPrivilegeDto>> GetRolePrivilegeByIdAsync(int id);
        Task<ApiResponse<GetAllPrivilegeDto>> GetRolePrivilegeByNameAsync(string name);
        Task<ApiResponse<bool>> CreateRolePrivilegeAsync(RolePrivilegeRequestDto roleDto);
        Task<ApiResponse<bool>> UpdateRolePrivilegeAsync(RolePrivilegeRequestDto roleDto);
        Task<ApiResponse<bool>> DeleteRolePrivilegeAsync(int id);
    }
}
