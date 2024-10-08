using System.Threading.Tasks;
using HotelManagement.Application.Contracts.Services;
using HotelManagement.Application.Dtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await _userService.GetRoles());
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(string role)
        {
            return Ok(await _userService.AddRole(role));
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(RegisterUserDto userDto)
        {
            return Ok(await _userService.AddUser(userDto));
        }

        [HttpPost("AddUserRole")]
        public async Task<IActionResult> AddUserRole(string userName,string role)
        {
            return Ok(await _userService.AddRoleToUser(userName,role));
        }

        [HttpGet("GetUserRole")]
        public async Task<IActionResult> GetUserRole(string userName)
        {
            return Ok(await _userService.GetUserRole(userName));
        }

        [HttpPost("RemoveUserRole")]
        public async Task<IActionResult> RemoveUserRole(string userName,string role)
        {
            return Ok(await _userService.RemoveUserRole(userName,role));

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string userName,string password)
        {
            return Ok(await _userService.Login(userName,password));
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUser)
        {
            return Ok(await _userService.UpdateUser(updateUser));
        }

    }
}