using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Interface;
using TaxCalculation.Application.Mapping;
using TaxCalculation.Domain.Entities;
using TaxCalculation.Domain.InterfaceInfrastructure;

namespace TaxCalculation.Application.Services
{
    public class RolePrivilegeService : IRolePrivilegeService
    {
        private readonly IRolePrivilegeRepository _roleRepository;
        private readonly IMapper _mapper;
        private ILogger<RolePrivilegeService> _logger;
        public RolePrivilegeService(IRolePrivilegeRepository roleRepository, IMapper mapper, ILogger<RolePrivilegeService> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<bool>> CreateRolePrivilegeAsync(RolePrivilegeRequestDto rolePrivilege)
        {
            try
            {
                if (rolePrivilege == null)
                {
                    // throw new ArgumentNullException(nameof(roleDto));
                    return ApiResponse<bool>.Fail($"All RolePrivilege required details not found.", 400);
                }

                var rolePrivileges = _mapper.Map<RolePrivilege>(rolePrivilege);
                rolePrivileges.IsDeleted = false;
                rolePrivileges.CreatedBy = rolePrivileges.RolePrivilegeName;
                rolePrivileges.CreatedDate = DateTime.UtcNow;
                rolePrivileges.ModifiedBy = rolePrivileges.RolePrivilegeName;
                rolePrivileges.ModifiedDate = DateTime.UtcNow;

                var rolePrivilegesResult = await _roleRepository.CreateRolePrivilegeAsync(rolePrivileges);
                if (!rolePrivilegesResult)
                {
                    return ApiResponse<bool>.Fail("Role creation failed.");
                }
               // var result = _mapper.Map<CreateRoleDto>(rolePrivilegesResult);
                return ApiResponse<bool>.Ok(true, "RolePrivilege created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError( ex, "Error creating user {rolePrivilege}", rolePrivilege);
                return ApiResponse<bool>.Fail($"An error occurred while fetching rolesPrivilege",500);
            }
        }

        public async Task<ApiResponse<bool>> DeleteRolePrivilegeAsync(int id)
        {
            if (id <= 0)
            {
                return ApiResponse<bool>.Fail($"Invalid RolePrivilege Id.", 400);
            }
            var result = await _roleRepository.DeleteRolePrivilegeAsync(id);
            if (!result)
            {
                return ApiResponse<bool>.Fail($"RolePrivilege deletion failed.", 500);
            }
            return ApiResponse<bool>.Ok(true, "RolePrivilege deleted successfully.");
        }

        public async Task<ApiResponse<List<GetAllPrivilegeDto>>> GetAllRolesPrivilegeAsync()
        {
            var rolesPrivilege = await _roleRepository.GetAllRolesPrivilegeAsync();
            var result = _mapper.Map<List<GetAllPrivilegeDto>>(rolesPrivilege);
            if (result == null || result.Count == 0)
            {
                return ApiResponse<List<GetAllPrivilegeDto>>.Fail("No rolePrivilege found.", 400);
            }
            return ApiResponse<List<GetAllPrivilegeDto>>.Ok(result, "Records fetched successfully.");
        }

        public async Task<ApiResponse<GetAllPrivilegeDto>> GetRolePrivilegeByIdAsync(int id)
        {
           if (id <= 0)
            {
                return ApiResponse<GetAllPrivilegeDto>.Fail($"Invalid RolePrivilege Id.", 400);
            }
            var rolePrivilege = await _roleRepository.GetRolePrivilegeByIdAsync(id);
            if (rolePrivilege == null)
            {
                return ApiResponse<GetAllPrivilegeDto>.Fail("RolePrivilege not found.", 404);
            }
            var result = _mapper.Map<GetAllPrivilegeDto>(rolePrivilege);
            return ApiResponse<GetAllPrivilegeDto>.Ok(result, "Record fetched successfully.");
        }

        public async Task<ApiResponse<GetAllPrivilegeDto>> GetRolePrivilegeByNameAsync(string name)
        {
           if (string.IsNullOrWhiteSpace(name))
            {
                return ApiResponse<GetAllPrivilegeDto>.Fail($"Invalid RolePrivilege Name.", 400);
            }
            var rolePrivilege = await _roleRepository.GetRolePrivilegeByNameAsync(name);
            if (rolePrivilege == null)
            {
                return ApiResponse<GetAllPrivilegeDto>.Fail("RolePrivilege not found.", 404);
            }
            var result = _mapper.Map<GetAllPrivilegeDto>(rolePrivilege);
            return ApiResponse<GetAllPrivilegeDto>.Ok(result, "Record fetched successfully.");
        }

        public async Task<ApiResponse<bool>> UpdateRolePrivilegeAsync(RolePrivilegeRequestDto rolePrivilegeDto)
        {
            if (rolePrivilegeDto == null || rolePrivilegeDto.Id <= 0)
            {
                return ApiResponse<bool>.Fail($"Invalid RolePrivilege details.", 400);
            }
            var existingRolePrivilege = await _roleRepository.GetRolePrivilegeByIdAsync(rolePrivilegeDto.Id);
            if (existingRolePrivilege == null)
            {
                return ApiResponse<bool>.Fail($"RolePrivilege with Id {rolePrivilegeDto.Id} not found.", 404);
            }

            existingRolePrivilege.RolePrivilegeName = rolePrivilegeDto.RolePrivilegeName;
            existingRolePrivilege.RoleDescription = rolePrivilegeDto.RoleDescription;
            existingRolePrivilege.ModifiedDate = DateTime.UtcNow;
            existingRolePrivilege.ModifiedBy = rolePrivilegeDto.RolePrivilegeName;
            existingRolePrivilege.IsDeleted = false;
            existingRolePrivilege.IsActive= rolePrivilegeDto.IsActive;
            var result = await _roleRepository.UpdateRolePrivilegeAsync(existingRolePrivilege);
            if (!result)
            {
                return ApiResponse<bool>.Fail($"RolePrivilege update failed.", 500);
            }
            return ApiResponse<bool>.Ok(true, "RolePrivilege updated successfully.");
        }
    }
}
