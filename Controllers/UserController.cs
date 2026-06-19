using CRUDPractice.BusinessLogic.Interfaces;
using CRUDPractice.Models.Dtos.UserDtos;
using CRUDPractice.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(
            IUserService userService)
        {
            _userService = userService;
            

        }

        [HttpPost]

        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var response = await _userService.CreateUserAsync(dto);

            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateUser(string id , UpdateUserDto dto)
        {
            var response = await _userService.UpdateUserAsync(id,dto);

            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
            

        }

        [HttpGet("All-users")]

        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("by-id/{id}")]

        public async Task<IActionResult> GetUserById(string id)
        {
            var response = await _userService.GetUserByIdAsync(id);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpGet("by-email/{email}")]

        public async Task <IActionResult> GetUserByEmail(string email)
        {
            var response = await _userService.GetUserByEmailAsync(email);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]

        public async Task <IActionResult> DeleteUser(string id)
        {
            var response = await _userService.DeleteUserAsync(id);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPatch("{id}/password")]

        public async Task <IActionResult> ChangePassword(string id , ChangePasswordDto dto)
        {
            var response = await _userService.ChangePasswordAsync(id, dto);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }
      


    }
}
