using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository.Interface
{
    public interface IUnitOfWork
    {
        Task CreateAsync<T>(T entity) where T : class;
        Task CreateRangeAsync<T>(List<T> entities) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task UpdateRangeAsync<T>(List<T> entities) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
        Task DeleteRangeAsync<T>(List<T> entities) where T : class;
        Task SaveAsync();
    }
}