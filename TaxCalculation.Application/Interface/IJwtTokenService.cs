using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Mapping;
using TaxCalculation.Domain.Entities;

namespace TaxCalculation.Application.Interface
{
    public interface IJwtTokenService
    {
        Task<ApiResponse<LoginResponseDto>> GenerateTokenAsync(UserDto user);
        Task<ApiResponse<LoginResponseDto?>> LoginAsync(LoginRequestDto request);
    }
}
