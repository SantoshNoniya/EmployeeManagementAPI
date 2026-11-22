using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Interface;
using TaxCalculation.Application.Mapping;
using TaxCalculation.Domain.Entities;
using TaxCalculation.Domain.InterfaceInfrastructure;

namespace TaxCalculation.Application.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _settings;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public JwtTokenService(IOptions<JwtSettings> options,IMapper mapper, IUserRepository userRepository, IUserService userService)
        {
            _settings = options.Value;
            _mapper = mapper;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<ApiResponse<LoginResponseDto>> GenerateTokenAsync(UserDto user)
        {
            try
            {
                var expiresAt = DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes);

                var claims = new List<Claim>
                {
                    new Claim(
                        ClaimTypes.NameIdentifier,
                        user.Id.ToString()),

                    new Claim(
                        ClaimTypes.Name,
                        user.UserName),

                    //new Claim(
                    //    ClaimTypes.Email,
                    //    user.Email),

                    //new Claim(
                    //    ClaimTypes.Role,
                    //    user.Role.Name)
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_settings.Key));

                var credentials =
                    new SigningCredentials(
                        key,
                         SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _settings.Issuer,
                    audience: _settings.Audience,
                    claims: claims,
                    expires: expiresAt,
                    signingCredentials: credentials);

                var accessToken =
                    new JwtSecurityTokenHandler()
                        .WriteToken(token);

                // AutoMapper mapping
                var responseUser =
                    _mapper.Map<ResponseUserDto>(user);

                var response = new LoginResponseDto
                {
                    AccessToken = accessToken,
                    ExpiresAt = expiresAt,
                    User = responseUser
                };

                return ApiResponse<LoginResponseDto>.Ok(
                    response,
                    "Login successful");

            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task<ApiResponse<LoginResponseDto?>> LoginAsync(LoginRequestDto request)
        {
            try
            {
                var user = await _userRepository.GetUserByNameAsync(request.Username);

                if (user == null)
                    return ApiResponse<LoginResponseDto?>.Fail("User not found");

                if (!user.IsActive)
                    return ApiResponse<LoginResponseDto?>.Fail("User is not active");

                var validPassword =
                    _userService.VerifyPassword(
                        request.Password,
                        user.Password,
                        user.PasswordSalt);

                if (!validPassword)
                    return ApiResponse<LoginResponseDto?>.Fail("Invalid password");

                // Map domain user to UserDto before generating token
                var userDto = _mapper.Map<UserDto>(user);

                var result = await GenerateTokenAsync(userDto);
                if (result == null || result.Data == null)
                    return ApiResponse<LoginResponseDto?>.Fail("Token generation failed");

                // Return the ApiResponse<LoginResponseDto> as ApiResponse<LoginResponseDto?>
                return ApiResponse<LoginResponseDto?>.Ok(result.Data, "Login successful");
            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
