    using CRUDPractice.BusinessLogic.Interfaces;
    using CRUDPractice.Models.Dtos.UserDtos;
    using CRUDPractice.Models.Entities;
    using CRUDPractice.Responses;
    using CRUDPractice.Validators.UserValidators;
    using FluentValidation;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.JSInterop.Infrastructure;
    using Microsoft.VisualBasic;
    using System.Diagnostics.CodeAnalysis;

    namespace CRUDPractice.BusinessLogic.Services
    {
        public class UserService : IUserService
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IValidator<CreateUserDto> _createValidator;
            private readonly IValidator<UpdateUserDto> _updateValidator;
            private readonly IValidator<ChangePasswordDto> _changePasswordValidator;

            public UserService(
                UserManager<ApplicationUser> userManager,
                IValidator<CreateUserDto> createValidator,
                IValidator<UpdateUserDto> updateValidator,
                IValidator<ChangePasswordDto> changePasswordValidator)
            {
                _userManager = userManager;
                _createValidator = createValidator;
                _updateValidator = updateValidator;
                _changePasswordValidator = changePasswordValidator;
            }

            public async Task<ApiResponse> CreateUserAsync(CreateUserDto dto)
            {
                var validationResult = await _createValidator.ValidateAsync(dto);

                if (!validationResult.IsValid)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "validation Error",
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                    };
                }

                var user = new ApplicationUser
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Age = dto.Age,
                    UserName = dto.Email
                };

                var result = await _userManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "User creation failed",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }

                return new ApiResponse
                {
                    Success = true,
                    Message = "user created successfully",
                };
            }

            public async Task<ApiResponse> UpdateUserAsync(string id, UpdateUserDto dto)
            {
                var validationResult = await _updateValidator.ValidateAsync(dto);

                if (!validationResult.IsValid)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Validation error",
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                    };
                }

                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "User doesn't exist"
                    };
                }

                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Email = dto.Email;
                user.Age = dto.Age;
                user.UserName = dto.Email;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "user couldn't be updated",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }

                return new ApiResponse
                {
                    Success = true,
                    Message = "User updated"
                };
            }

            public async Task<ApiResponse<List<UserDto>>> GetAllUsersAsync()
            {
                var users = await _userManager.Users
                   .Select(user => new UserDto
                   {
                       Id = user.Id,
                       FirstName = user.FirstName,
                       LastName = user.LastName,
                       Age = user.Age,
                       Email = user.Email!
                   })
                   .ToListAsync();

                return new ApiResponse<List<UserDto>>
                {
                    Success = true,
                    Message = "users retrived successfully",
                    Data = users
                };
            }

            public async Task<ApiResponse<UserDto>> GetUserByIdAsync(string id)
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user is null)
                {
                    return new ApiResponse<UserDto>
                    {
                        Success = false,
                        Message = "User doesn't exist"
                    };
                }

                return new ApiResponse<UserDto>
                {
                    Success = true,
                    Message = "User retrieved successfully.",
                    Data = new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Age = user.Age,
                        Email = user.Email!
                    }
                };
            }

            public async Task<ApiResponse<UserDto>> GetUserByEmailAsync(string email)
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user is null)
                {
                    return new ApiResponse<UserDto>
                    {
                        Success = false,
                        Message = "user doesn't exist",
                    };
                }

                return new ApiResponse<UserDto>
                {
                    Success = true,
                    Message = "User retreived successfully",
                    Data = new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Age = user.Age,
                        Email = user.Email!
                    }
                };
            }

            public async Task<ApiResponse> DeleteUserAsync(string id)
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user is null)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "User doesn't exist",
                    };
                }

                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Couldn't delete the user",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }

                return new ApiResponse
                {
                    Success = true,
                    Message = "User delete successfully",
                };
            }

            public async Task<ApiResponse> ChangePasswordAsync(string id, ChangePasswordDto dto)
            {
                var validationResult = await _changePasswordValidator.ValidateAsync(dto);

                if (!validationResult.IsValid)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Validation Error",
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                    };
                }

                var user = await _userManager.FindByIdAsync(id);

                if (user is null)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "User doesn't exist",
                    };
                }

                var result = await _userManager.ChangePasswordAsync(
                    user, dto.CurrentPassword, dto.NewPassword);

                if (!result.Succeeded)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Couldn't change the password",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }

                return new ApiResponse
                {
                    Success = true,
                    Message = "Password changed successfully"
                };
            }
        }
    }