using ExpenseTracker.Enums;

namespace ExpenseTracker.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public TransactionType TransactionType { get; set; }
        public string? Icon { get; set; }
    }
}