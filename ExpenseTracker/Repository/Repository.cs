using System.Linq.Expressions;
using ExpenseTracker.Data;
using ExpenseTracker.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
           return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id) 
                ?? throw new Exception("Not found");
        }
    }
}