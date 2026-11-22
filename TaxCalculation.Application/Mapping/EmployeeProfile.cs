using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Domain.Entities;

namespace TaxCalculation.Application.Mapping
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Role, GetRoleDetailsDto>().ReverseMap();
            CreateMap<Role, CreateRoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ResponseUserDto,User>().ReverseMap();
            CreateMap<User, LoginResponseDto>()
            .ForMember(
                dest => dest.AccessToken,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.ExpiresAt,
                opt => opt.Ignore());
            CreateMap<UserDto, ResponseUserDto>().ReverseMap();
            CreateMap<RolePrivilegeRequestDto, RolePrivilege>().ReverseMap();
            CreateMap<GetAllPrivilegeDto, RolePrivilege>().ReverseMap();


        }
    }
}
