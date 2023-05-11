namespace ExpenseTracker.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public double Amount { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}