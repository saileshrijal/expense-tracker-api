using ExpenseTracker.Enums;

namespace ExpenseTracker.Dtos
{
    public class CategoryDto
    {
        public string? Name { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}