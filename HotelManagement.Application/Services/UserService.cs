using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.Services;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Dtos.Request;
using HotelManagement.Application.Helpers;
using HotelManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;

namespace HotelManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork,RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> AddRole(string role)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role);
            if (roleExist)
            {
                return "Role already exist";
            }

            var addRole = await _roleManager.CreateAsync(new IdentityRole(role));
            if(addRole.Succeeded)
            {
                return "Role added successfully";
            }

            return "Something went wrong";
        }

        public async  Task<List<IdentityRole>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        
        public async Task<string> AddUser(RegisterUserDto userDto)
        {

            var newUser = new User()
            {
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                UserName = userDto.UserName,
            };
            var addUser = await _userManager.CreateAsync(newUser, userDto.Password);
            if(addUser.Succeeded)
            {

                return "User Successfully Created";
            }

            return "Bad Request";
        }

        public async Task<string> AddRoleToUser(string userName, string role)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return "Bad Request";
            }
        var roleExist = await _roleManager.RoleExistsAsync(role);
        if(!roleExist)
        {
            return "Role Doest exist";
        }
            var addRole = await _userManager.AddToRoleAsync(user, role);
            if(addRole.Succeeded)
            {
                return "Role Added Successfully";
            }

            return "Something went wrong";
        }
        public async Task<IList<string>> GetUserRole(string userName)
        {
             var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return new List<string>();
            }
            var userRole = await _userManager.GetRolesAsync(user);
            return userRole;
        }

        public async Task<string> RemoveUserRole(string userName, string role)
        {
             var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return "Bad Request";
            }
            var removeUserRole = await _userManager.RemoveFromRoleAsync(user, role);
            if(removeUserRole.Succeeded)
            {
                return "Success";
            }

            return "Something Went Wrong";
        }

        public async Task<string> Login(string userName, string password)
        {
            var user =  await _userManager.FindByNameAsync(userName);
            if(user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return "Bad request";
            }
            
            var token =  await JWTGenerator.GetToken(_userManager,_configuration,user);
            var finalToken = new JwtSecurityTokenHandler().WriteToken(token);
            return finalToken;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var allUsers = await _unitOfWork.UserRepository.GetAll();
            return allUsers;
        }

        public async Task<string> UpdateUser(UpdateUserDto requestDto)
        {
            var userExist = await _userManager.FindByIdAsync(requestDto.Id);
            if(userExist == null)
            {
                return "Bad Request";
            }

            userExist.Email = requestDto.Email;
            userExist.PhoneNumber = requestDto.PhoneNumber;
            userExist.UserName = requestDto.UserName;

             _unitOfWork.UserRepository.UpdateASync(userExist);
            var save = await _unitOfWork.Save();
            if(save > 0)
            {
                return "Update Successfull";
            }

            return "Something went wrong";
        }


    }
    
}