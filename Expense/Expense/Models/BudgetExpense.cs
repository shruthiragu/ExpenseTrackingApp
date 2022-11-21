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

        public string ExpenseCategoryIconFile { get; set; }
        public string FileName { get; set; }
        
        public BudgetExpense(string category)
        {
            switch (category)
            {
                case "Grocery":
                    ExpenseCategoryIconFile = "icon_grocery.png";
                    break;
                case "Travel":
                    ExpenseCategoryIconFile = "icon_travel.png";
                    break;
                case "Shopping":
                    ExpenseCategoryIconFile = "icon_shopping.png";
                    break;
                case "Misc":
                    ExpenseCategoryIconFile = "icon_misc.png";
                    break;
            }
        }
    }
}
