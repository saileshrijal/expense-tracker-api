using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Results;

namespace ExpenseTracker.Manager.Interface
{
    public interface IAuthManager
    {
        Task<AuthResult> Login(string username, string password);
    }
}