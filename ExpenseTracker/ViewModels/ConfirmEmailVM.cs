using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.ViewModels
{
    public class ConfirmEmailVM
    {
        [Required]
        public string? UserId { get; set; }
        
        [Required]
        public string? Token { get; set; }
    }
}