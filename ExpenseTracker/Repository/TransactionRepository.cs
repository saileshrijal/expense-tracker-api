
using ExpenseTracker.Data;
using ExpenseTracker.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repository
{
    public class TransactionRepository : Repository<Models.Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<Models.Transaction>> GetTransactionsByUserId(string userId)
        {
            return await _context.Transactions!
                .Where(x => x.ApplicationUserId == userId)
                .Include(x=>x.Category)
                .OrderByDescending(x=>x.CreatedDate)
                .ToListAsync();
        }
    }
}