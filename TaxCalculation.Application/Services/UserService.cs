using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Interface;
using TaxCalculation.Application.Mapping;
using TaxCalculation.Domain.Entities;
using TaxCalculation.Domain.InterfaceInfrastructure;

namespace TaxCalculation.Application.Services
{
    public class UserService:IUserService
    {
        private readonly  IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int IterationCount = 100_000;
        public UserService(IMapper mapper, IUserRepository userRepository,ILogger<UserService> logger) 
        {   
            _mapper = mapper;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> CreateUserAsync( UserDto userDto)
        {
            if (userDto == null)
            {
                return ApiResponse<bool>.Fail(
                    "User details are required.",
                    400);
            }

            try
            {
                // -------------------------
                // Validate username
                // -------------------------

                if (string.IsNullOrWhiteSpace(userDto.UserName))
                {
                    return ApiResponse<bool>.Fail(
                        "Username is required.",
                        400);
                }

                var userName = userDto.UserName.Trim();

                // -------------------------
                // Validate password
                // -------------------------

                if (string.IsNullOrWhiteSpace(userDto.Password))
                {
                    return ApiResponse<bool>.Fail(
                        "Password is required.",
                        400);
                }

                // -------------------------
                // Check duplicate username
                // -------------------------

                var existingUser =
                    await _userRepository.GetUserByNameAsync(userName);

                if (existingUser != null)
                {
                    return ApiResponse<bool>.Fail(
                        "The username is already taken.",
                        409);
                }

                // -------------------------
                // Map DTO -> Entity
                // -------------------------

                var user = _mapper.Map<User>(userDto);

                user.UserName = userName;

                user.IsDeleted = false;

                user.CreatedDate = DateTime.UtcNow;
                user.ModifiedDate = DateTime.UtcNow;

                user.CreatedBy = userName;
                user.ModifiedBy = userName;

                // -------------------------
                // Password hashing
                // -------------------------

                var passwordResult = CreatePasswordHashWithSalt(userDto.Password);

                user.Password = passwordResult.PasswordHash;
                user.PasswordSalt = passwordResult.Salt;

                // -------------------------
                // Create user
                // -------------------------

                var createdUser =
                    await _userRepository.CreateUserAsync(user);

                if (!createdUser)
                {
                    return ApiResponse<bool>.Fail(
                        "User creation failed.",
                        500);
                }


                return ApiResponse<bool>.Ok(
                    true,
                    "User created successfully.",
                    201);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating user {UserName}",
                    userDto.UserName);

                return ApiResponse<bool>.Fail(
                    "An unexpected error occurred while creating the user.",
                    500);
            }
        }

        public async Task<ApiResponse<bool>> UpdateUserAsync(UserDto userDto)
        {
            
            try
            {
                if (userDto == null)
                {
                    return ApiResponse<bool>.Fail(
                        "User details are required.",
                        400);
                }
                var existingUser =  await _userRepository.GetUserByIdAsync(userDto.Id);

                if (existingUser == null)
                {
                    return ApiResponse<bool>.Fail(
                        "User not found.",
                        400);
                }

                // -------------------------
                // Map DTO -> Entity
                // -------------------------

                // var user = _mapper.Map<User>(userDto);

                existingUser.UserName = userDto.UserName.Trim();
                existingUser.UserTypeId = userDto.UserTypeId;
                existingUser.IsActive = userDto.IsActive;
                existingUser.IsDeleted = false;
                existingUser.ModifiedDate = DateTime.UtcNow;
                existingUser.ModifiedBy = userDto.UserName.Trim();

                // -------------------------
                // Password hashing
                // -------------------------

                if (!string.IsNullOrWhiteSpace(userDto.Password))
                {
                    var passwordResult = CreatePasswordHashWithSalt(userDto.Password);
                    existingUser.Password = passwordResult.PasswordHash;
                    existingUser.PasswordSalt = passwordResult.Salt;
                }

                // -------------------------
                // Create user
                // -------------------------

                var createdUser =
                    await _userRepository.UpdateUserAsync(existingUser);

                if (!createdUser)
                {
                    return ApiResponse<bool>.Fail(
                        "User update failed.",
                        500);
                }


                return ApiResponse<bool>.Ok(
                    true,
                    "User updated successfully.",
                    200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error update user creation {userDto}", userDto);

                return ApiResponse<bool>.Fail(
                    "An unexpected error occurred while creating the user.",
                    500);
            }
        }

        public async Task<ApiResponse<bool>> DeleteRoleAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ApiResponse<bool>.Fail($"Invalid user ID.", 400);
                }
                var existingUser = await _userRepository.GetUserByIdAsync(id);

                if (existingUser == null)
                {
                    return ApiResponse<bool>.Fail(
                        $"User with ID {id} not found.",
                        404);
                }

                var result = await _userRepository.DeleteUserAsync(id);

                if (!result)
                {
                    return ApiResponse<bool>.Fail($"Failed to delete user with ID {id}.",500);
                }

                return ApiResponse<bool>.Ok(true, $"User with ID {id} deleted successfully.");

            }
            catch (Exception ex)
            {

                _logger.LogError(ex,"Error deleting user with ID {UserId}",id);

                return ApiResponse<bool>.Fail(
                    "An unexpected error occurred while deleting the user.",
                    500);
            }
        }
        public async Task<ApiResponse<ResponseUserDto>> GetUserByNameAsync(string name)
        {
            try
            {
                var userName= name.Trim();
                var userDetails = await _userRepository.GetUserByNameAsync(userName);

                if (userDetails == null)
                {
                    return ApiResponse<ResponseUserDto>.Fail($"User with ID {name} not found.");
                }
                var result = _mapper.Map<ResponseUserDto>(userDetails);

                return ApiResponse<ResponseUserDto>.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error getting user {UserName}",name);
                return ApiResponse<ResponseUserDto>.Fail($"An error occurred while fetching roles",500);
            }

        }

        public async Task<ApiResponse<ResponseUserDto>> GetUserByIdAsync(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return ApiResponse<ResponseUserDto>.Fail($"Please enter valid ID {id}.");
                }
                var userDetails = await _userRepository.GetUserByIdAsync(id);

                if (userDetails == null)
                {
                    return ApiResponse<ResponseUserDto>.Fail($"User with ID {id} not found.");
                }
                var result = _mapper.Map<ResponseUserDto>(userDetails);

                return ApiResponse<ResponseUserDto>.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user {UserId}", id);
                return ApiResponse<ResponseUserDto>.Fail($"An error occurred while fetching roles", 500);
            }

        }

        public async Task<ApiResponse<List<ResponseUserDto>>> GetAllUserAsync()
        {
            try
            {
                
                var userDetails = await _userRepository.GetUserAllAsync();

                if (userDetails == null)
                {
                    return ApiResponse <List<ResponseUserDto>>.Fail($"Records not found.",400);
                }
                var result = _mapper.Map<List<ResponseUserDto>>(userDetails);

                return ApiResponse<List<ResponseUserDto>>.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting while fetching records");
                return ApiResponse<List<ResponseUserDto>>.Fail($"An error occurred while fetching records", 500);
            }

        }
        public (string PasswordHash, string Salt) CreatePasswordHashWithSalt(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(
                    "Password cannot be empty.",
                    nameof(password));
            }

            // Generate cryptographically secure salt
            var salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            // Generate PBKDF2 hash
            var hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: IterationCount,
                numBytesRequested: HashSize);

            return (
                PasswordHash: Convert.ToBase64String(hash),
                Salt: Convert.ToBase64String(salt)
            );
        }

        public bool VerifyPassword(string password,string storedHash,string storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(storedHash) ||
                string.IsNullOrWhiteSpace(storedSalt))
            {
                return false;
            }

            try
            {
                var salt = Convert.FromBase64String(storedSalt);
                var originalHash = Convert.FromBase64String(storedHash);

                var computedHash = KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: IterationCount,
                    numBytesRequested: HashSize);

                return CryptographicOperations.FixedTimeEquals(
                    originalHash,
                    computedHash);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
