using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Dtos;
using ExpenseTracker.Results;

namespace ExpenseTracker.Services.Interface
{
    public interface IUserService
    {
        Task CreateAsync(UserDto userDto);
        Task ChangePassword(string password);
        Task ResetPassword(string password, string token);
        Task ResetEmail(string email, string token);
    }
}