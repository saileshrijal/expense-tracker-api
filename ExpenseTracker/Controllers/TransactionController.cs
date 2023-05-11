using ExpenseTracker.Dtos;
using ExpenseTracker.Provider.Interface;
using ExpenseTracker.Repository.Interface;
using ExpenseTracker.Services.Interface;
using ExpenseTracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        public TransactionController(ITransactionService transactionService, ITransactionRepository transactionRepository, ICurrentUserProvider currentUserProvider)
        {
            _transactionService = transactionService;
            _transactionRepository = transactionRepository;
            _currentUserProvider = currentUserProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var transactions = await _transactionRepository.GetTransactionsByUserId(await _currentUserProvider.GetUserIdAsync());
                var results = transactions.Select(x => new
                {
                    x.Id,
                    category = new
                    {
                        x.Category!.Id,
                        x.Category.Name,
                        TransactionType = new
                        {
                            id = x.Category.TransactionType,
                            value = x.Category.TransactionType.ToString()
                        }
                    },
                    x.Amount,
                    ApplicationUser = new
                    {
                        x.ApplicationUser!.Id,
                        x.ApplicationUser.UserName,
                        x.ApplicationUser.Email
                    },
                    x.Note
                });
                
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var transaction = await _transactionRepository.GetByIdAsync(id);
                if(transaction.ApplicationUserId != await _currentUserProvider.GetUserIdAsync())
                    return Unauthorized();
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionVM vm)
        {
            try
            {
                var transactionDto = new TransactionDto()
                {
                    Amount = vm.Amount,
                    CategoryId = vm.CategoryId,
                    Note = vm.Note,
                };
                await _transactionService.CreateAsync(transactionDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TransactionVM vm)
        {
            try
            {
                var transactionDto = new TransactionDto()
                {
                    Amount = vm.Amount,
                    CategoryId = vm.CategoryId,
                    Note = vm.Note,
                };
                await _transactionService.UpdateAsync(id, transactionDto);
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
                await _transactionService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}