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
                ApplicationUserId = await _currentUserProvider.GetUserIdAsync()
            };
            await _unitOfWork.CreateAsync(category);
        }

        public Task DeleteAsync(int id)
        {
            var category = _categoryRepository.GetByIdAsync(id);
            if(category == null)
            {
                throw new Exception("Category not found");
            }
            return _unitOfWork.DeleteAsync(category);
        }

        public async Task UpdateAsync(int id, CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if(category == null)
            {
                throw new Exception("Category not found");
            }
            category.Name = categoryDto.Name;
            await _unitOfWork.UpdateAsync(category);
        }
    }
}