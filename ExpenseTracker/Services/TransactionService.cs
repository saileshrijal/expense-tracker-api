using ExpenseTracker.Dtos;
using ExpenseTracker.Provider.Interface;
using ExpenseTracker.Repository.Interface;
using ExpenseTracker.Services.Interface;

namespace ExpenseTracker.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICurrentUserProvider _currentUserProvider;

        public TransactionService(IUnitOfWork unitOfWork,
                                ITransactionRepository transactionRepository,
                                ICurrentUserProvider currentUserProvider)
        {
            _unitOfWork = unitOfWork;
            _transactionRepository = transactionRepository;
            _currentUserProvider = currentUserProvider;
        }

        public async Task CreateAsync(TransactionDto transactionDto)
        {
            var transaction = new Models.Transaction(){
                Amount = transactionDto.Amount,
                CreatedDate = DateTime.UtcNow,
                ApplicationUserId = await _currentUserProvider.GetUserIdAsync(),
                CategoryId = transactionDto.CategoryId,
                Note = transactionDto.Note,
            };
            await _unitOfWork.CreateAsync(transaction);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if(transaction.ApplicationUserId != await _currentUserProvider.GetUserIdAsync())
                throw new UnauthorizedAccessException("You are not authorized to delete this transaction.");
            await _unitOfWork.DeleteAsync(transaction);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(int id, TransactionDto transactionDto)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if(transaction.ApplicationUserId != await _currentUserProvider.GetUserIdAsync())
                throw new UnauthorizedAccessException("You are not authorized to update this transaction.");
            transaction.Amount = transactionDto.Amount;
            transaction.CategoryId = transactionDto.CategoryId;
            transaction.Note = transactionDto.Note;
            await _unitOfWork.SaveAsync();
        }
    }
}