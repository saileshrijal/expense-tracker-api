using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Provider.Interface;
using Microsoft.AspNetCore.Identity;
using ExpenseTracker.Models;

namespace ExpenseTracker.Provider
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor, 
                                    UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<string> GetUserIdAsync()
        {
            var userIdClaim = _httpContextAccessor.HttpContext
                                !.User.FindFirst(ClaimTypes.NameIdentifier)
                                ?? throw new Exception("User not found");
            var user = await _userManager.FindByNameAsync(userIdClaim.Value);
            return user!.Id;
        }
    }
}