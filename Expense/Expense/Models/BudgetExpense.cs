using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Models
{
    public class BudgetExpense
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public DateTime DatePurchased { get; set; }
        public string ExpenseCategory { get; set; }        
        public string FileName { get; set; }        
    }
}
