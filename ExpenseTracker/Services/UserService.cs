using ExpenseTracker.Dtos;
using ExpenseTracker.Models;
using ExpenseTracker.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task CreateAsync(UserDto userDto)
        {
            await Validate(userDto.UserName, userDto.Email);
            var user = new ApplicationUser(){
                UserName = userDto.UserName,
                Email = userDto.Email,
            };
            await _userManager.CreateAsync(user, userDto.Password!);
        }


        private async Task Validate(string? username, string? email)
    {
        if (await _userManager.FindByNameAsync(username!) != null)
        {
            throw new Exception($"Username {username} already taken. Please use another username");
        }
        if (await _userManager.FindByEmailAsync(email!) != null)
        {
            throw new Exception($"Email {email} already taken. Please use another email");
        }
    }
    }
}