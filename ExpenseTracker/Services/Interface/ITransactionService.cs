using ExpenseTracker.Dtos;

namespace ExpenseTracker.Services.Interface
{
    public interface ITransactionService
    {
        Task CreateAsync(TransactionDto transactionDto);
        Task UpdateAsync(int id, TransactionDto transactionDto);
        Task DeleteAsync(int id);
    }
}