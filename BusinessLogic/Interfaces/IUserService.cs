using CRUDPractice.Models.Dtos.UserDtos;
using CRUDPractice.Responses;

namespace CRUDPractice.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse> CreateUserAsync(CreateUserDto dto);
        Task<ApiResponse> UpdateUserAsync(string id,UpdateUserDto dto);   
        Task<ApiResponse<List<UserDto>>> GetAllUsersAsync();
        Task<ApiResponse<UserDto>> GetUserByIdAsync(string id);
        Task<ApiResponse<UserDto>> GetUserByEmailAsync(string email);
        Task<ApiResponse> DeleteUserAsync(string id);
        Task<ApiResponse> ChangePasswordAsync(string id , ChangePasswordDto dto);
    }
}
