using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetCategoriesByUserIdAsync(string userId)
        {
            return await _context.Categories!.Where(c => c.ApplicationUserId == userId)
                    .OrderByDescending(x=>x.CreatedDate)
                    .ToListAsync();
        }
    }
}