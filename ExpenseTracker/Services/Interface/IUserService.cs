using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Dtos;

namespace ExpenseTracker.Services.Interface
{
    public interface IUserService
    {
        Task CreateAsync(UserDto userDto);
    }
}