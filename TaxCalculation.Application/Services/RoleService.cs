using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Interface;
using TaxCalculation.Application.Mapping;
using TaxCalculation.Domain.Entities;
using TaxCalculation.Domain.InterfaceInfrastructure;

namespace TaxCalculation.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly Microsoft.Extensions.Logging.ILogger<RoleService> _logger;

        public RoleService(
            IRoleRepository roleRepository,
            IMapper mapper,
            Microsoft.Extensions.Logging.ILogger<RoleService> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<CreateRoleDto>> CreateRoleAsync(CreateRoleDto roleDto)
        {
            try
            {
                if (roleDto == null)
                {
                   // throw new ArgumentNullException(nameof(roleDto));
                    return ApiResponse<CreateRoleDto>.Fail($"All Role required details not found.",400);
                }

                var role = _mapper.Map<Role>(roleDto);
                role.IsDeleted = false;
                role.CreatedBy = role.RoleName;
                role.CreatedDate = DateTime.UtcNow;
                role.ModifiedBy = role.RoleName;
                role.ModifiedDate = DateTime.UtcNow;

                var roleResult = await _roleRepository.CreateRoleAsync(role);
                if (roleResult == null)
                {
                    return ApiResponse<CreateRoleDto>.Fail("Role creation failed.");
                }
               // roleDto.Id = roleResult.Id;
                var result= _mapper.Map<CreateRoleDto>(roleResult);
                return ApiResponse<CreateRoleDto>.Ok(result, "Role created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating role {RoleName}", roleDto?.RoleName);
                return ApiResponse<CreateRoleDto>.Fail("An unexpected error occurred while creating the role.", 500);
            }

        }

        public async Task<ApiResponse<List<GetRoleDetailsDto>>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _roleRepository.GetAllRolesAsync();
                var result =_mapper.Map<List<GetRoleDetailsDto>>(roles);
                if (result == null || result.Count == 0)
                {
                    return ApiResponse<List<GetRoleDetailsDto>>.Fail("No roles found.");
                }

                return ApiResponse<List<GetRoleDetailsDto>>.Ok(result, "Records fetched successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching roles");
                return ApiResponse<List<GetRoleDetailsDto>>.Fail("An unexpected error occurred while fetching roles.", 500);
            }

        }

        public async Task<ApiResponse<GetRoleDetailsDto>> GetRoleByIdAsync(int id)
        {
            try
            {
                var role = await _roleRepository.GetRoleByIdAsync(id);

                if (role == null)
                {
                    return ApiResponse<GetRoleDetailsDto>.Fail($"Role with ID {id} not found.");
                }
               var result=_mapper.Map<GetRoleDetailsDto>(role);

               return ApiResponse<GetRoleDetailsDto>.Ok(result, "Records fetched successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching role by ID {RoleId}", id);
                return ApiResponse<GetRoleDetailsDto>.Fail("An unexpected error occurred while fetching the role.", 500);
            }

        }

        public async Task<ApiResponse<GetRoleDetailsDto>> GetRoleByNameAsync(string name)
        {
            try
            {
                var roleName = await _roleRepository.GetRoleByNameAsync(name);

                if (roleName == null)
                {
                    return ApiResponse<GetRoleDetailsDto>.Fail($"Role with Name {name} not found.");
                }
                var result = _mapper.Map<GetRoleDetailsDto>(roleName);

                return ApiResponse<GetRoleDetailsDto>.Ok(result, "Records fetched successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching role by name {RoleName}", name);
                return ApiResponse<GetRoleDetailsDto>.Fail("An unexpected error occurred while fetching the role.", 500);
            }

        }
        public async Task<ApiResponse<RoleDto>> UpdateRoleAsync(RoleDto roleDto)
        {
            try
            {
                if (roleDto == null)
                {
                    return ApiResponse<RoleDto>.Fail(
                        "Role data is required.",
                        400,
                        null);
                }

                if (roleDto.Id <= 0)
                {
                    return ApiResponse<RoleDto>.Fail(
                        "Invalid role ID.",
                        400,
                        null);
                }
                var existingRole = await _roleRepository.GetRoleByIdAsync(roleDto.Id);
                if (existingRole == null)
                {
                    return ApiResponse<RoleDto>.Fail("Failed to update the role.",400);
                }
                existingRole.RoleName = roleDto.RoleName;
                existingRole.RoleDescription = roleDto.RoleDescription;
                existingRole.IsActive = roleDto.IsActive;
                existingRole.ModifiedBy = "Admin";
                existingRole.ModifiedDate = DateTime.UtcNow;
                var updatedRole = await _roleRepository.UpdateRoleAsync(existingRole);
                var result= _mapper.Map<RoleDto>(updatedRole);
                return ApiResponse<RoleDto>.Ok(result,"Role updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating role {RoleId}", roleDto?.Id);
                return ApiResponse<RoleDto>.Fail("An unexpected error occurred while updating the role.", 500);
            }
        }

        public async Task<ApiResponse<bool>> DeleteRoleAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ApiResponse<bool>.Fail($"Not found {id}", 400, null);
                }
                var existingRole = await _roleRepository.GetRoleByIdAsync(id);

                if (existingRole == null)
                {
                    return ApiResponse<bool>.Fail($"Role with ID {id} not found.", 404);
                }

                var result = await _roleRepository.DeleteRoleAsync(existingRole.Id);

                if (!result)
                {
                    return ApiResponse<bool>.Fail($"Failed to delete role with ID {id}.");
                }

                return ApiResponse<bool>.Ok(true,$"Role with ID {id} deleted successfully.");
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting role {RoleId}", id);
                return ApiResponse<bool>.Fail("An unexpected error occurred while deleting the role.", 500);
            }
        }

    }
}
