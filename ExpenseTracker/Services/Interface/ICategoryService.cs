using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Dtos;

namespace ExpenseTracker.Services.Interface
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryDto categoryDto);
        Task UpdateAsync(int id, CategoryDto categoryDto);
        Task DeleteAsync(int id);
    }
}