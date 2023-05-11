using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Provider.Interface
{
    public interface ICurrentUserProvider
    {
        Task<string> GetUserIdAsync();
    }
}