using ExpenseTracker.Dtos;
using ExpenseTracker.Models;
using ExpenseTracker.Provider.Interface;
using ExpenseTracker.Repository.Interface;
using ExpenseTracker.Services.Interface;

namespace ExpenseTracker.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        public CategoryService(IUnitOfWork unitOfWork,
                        ICategoryRepository categoryRepository,
                        ICurrentUserProvider currentUserProvider)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _currentUserProvider = currentUserProvider;
        }
        public async Task CreateAsync(CategoryDto categoryDto)
        {
            var category = new Category(){
                Name = categoryDto.Name,
                CreatedDate = DateTime.Now,
                ApplicationUserId = await _currentUserProvider.GetUserIdAsync(),
                TransactionType = categoryDto.TransactionType
            };
            await _unitOfWork.CreateAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = _categoryRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(int id, CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            category.Name = categoryDto.Name;
            category.TransactionType = categoryDto.TransactionType;
            await _unitOfWork.SaveAsync();
        }
    }
}