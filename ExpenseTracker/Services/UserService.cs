using ExpenseTracker.Dtos;
using ExpenseTracker.Models;
using ExpenseTracker.Provider.Interface;
using ExpenseTracker.Results;
using ExpenseTracker.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserProvider _currentUserProvider;

        public UserService(UserManager<ApplicationUser> userManager,
                            ICurrentUserProvider currentUserProvider)
        {
            _userManager = userManager;
            _currentUserProvider = currentUserProvider;
        }

        public async Task ChangePassword(string password)
        {
            var user = await _userManager.FindByIdAsync(await _currentUserProvider.GetUserIdAsync())
                                    ?? throw new Exception("User not found");
            await _userManager.ChangePasswordAsync(user!, user.PasswordHash!, password);
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

        public async Task ResetEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email)
                                    ?? throw new Exception("User not found");
            await _userManager.ChangeEmailAsync(user, user.Email!, token);
        }

        public Task ResetPassword(string password, string token)
        {
            var user = _userManager.FindByIdAsync(_currentUserProvider.GetUserIdAsync().Result).Result
                                    ?? throw new Exception("User not found");
            return _userManager.ResetPasswordAsync(user, token, password);
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