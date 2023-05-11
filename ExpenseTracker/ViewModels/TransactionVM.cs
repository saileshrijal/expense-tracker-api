using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.ViewModels
{
    public class TransactionVM
    {
        public int CategoryId { get; set; }
        public double Amount { get; set; }
        public string? Note { get; set; }
    }
}