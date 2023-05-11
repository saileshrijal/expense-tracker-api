using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Enums;

namespace ExpenseTracker.ViewModels
{
    public class CategoryVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Transaction type is required")]
        public TransactionType TransactionType { get; set; }
    }
}