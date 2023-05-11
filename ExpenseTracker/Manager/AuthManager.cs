using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Helpers;
using ExpenseTracker.Helpers.Inteface;
using ExpenseTracker.Manager.Interface;
using ExpenseTracker.Models;
using ExpenseTracker.Results;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Manager
{
    public class AuthManager : IAuthManager
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthManager(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<AuthResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new AuthResult
                {
                    Errors = new List<string>
                    {
                        "User does not exist"
                    }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!userHasValidPassword)
            {
                return new AuthResult
                {
                    Errors = new List<string>
                    {
                        "Password is incorrect"
                    }
                };
            }

            if (!user.EmailConfirmed)
            {
                return new AuthResult
                {
                    Errors = new List<string>
                    {
                        "Email is not confirmed"
                    }
                };
            }

            return new AuthResult
            {
                Success = true,
                Token = _jwtService.GenerateToken(user)
            };
        }
    }
}