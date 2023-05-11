using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Dtos;
using ExpenseTracker.Provider.Interface;
using ExpenseTracker.Repository.Interface;
using ExpenseTracker.Services.Interface;
using ExpenseTracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        public CategoryController(ICategoryService categoryService,
                            ICategoryRepository categoryRepository,
                            ICurrentUserProvider currentUserProvider)
        {
            _categoryService = categoryService;
            _categoryRepository = categoryRepository;
            _currentUserProvider = currentUserProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryRepository.GetCategoriesByUserIdAsync(await _currentUserProvider.GetUserIdAsync());
                var results = categories.Select(c => new {
                    c.Id,
                    c.Name,
                    // getting transactionType text from enum value
                    TransactionType = new{
                        id = c.TransactionType,
                        name = c.TransactionType.ToString()
                    },
                    c.ApplicationUserId
                });
                // get text from enum value
                // var transactionType = categories.Select(c => c.TransactionType.ToString());

                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryVM vm)
        {
            try
            {
                var categoryDto = new CategoryDto(){
                    Name = vm.Name,
                    TransactionType = vm.TransactionType
                };
                await _categoryService.CreateAsync(categoryDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryVM vm)
        {
            try
            {
                var categoryDto = new CategoryDto(){
                    Name = vm.Name,
                    TransactionType = vm.TransactionType
                };
                await _categoryService.UpdateAsync(id, categoryDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]    
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}