using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Models
{
    public enum ExpenseCategory
    {
        Grocery,
        Travel,
        Shopping,
        Misc
    }
    public class BudgetExpense
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public DateTime DatePurchased { get; set; }
        public ExpenseCategory Category{ get; set; }
        public string FileName { get; set; }        
    }
}
