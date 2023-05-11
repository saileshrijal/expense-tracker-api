using System.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository.Interface
{
    public interface ITransactionRepository : IRepository<Models.Transaction>
    {
        Task<List<Models.Transaction>> GetTransactionsByUserId(string userId);
    }
}