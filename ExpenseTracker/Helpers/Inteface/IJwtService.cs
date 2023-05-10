using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Models;

namespace ExpenseTracker.Helpers.Inteface
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);
    }
}