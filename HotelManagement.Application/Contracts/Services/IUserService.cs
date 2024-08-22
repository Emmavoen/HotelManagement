using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Application.Dtos.Request;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Application.Contracts.Services
{
    public interface IUserService
    {
        Task<string> AddRole(string role);
        Task<List<IdentityRole>> GetRoles();

        Task<string> AddUser(RegisterUserDto userDto);
        Task<string> AddRoleToUser(string useName, string role);
        Task<IList<string>> GetUserRole(string userName);
        Task<string> RemoveUserRole(string userName, string role);
        Task<string> Login(string userName, string password);
    }
    
}