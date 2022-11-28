using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Expense.Views;
using System.IO;
using System.Text.RegularExpressions;

namespace Expense
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public ShellContent MainPageContent;
        public ShellContent AddExpenseContent;
        public static int BudgetAmt;
        public static int TotalExpenses;
        public static int TotalGroceryExpenses;
        public static int TotalTravelExpenses;
        public static int TotalShoppingExpenses;
        public static int TotalMiscExpenses;

        public AppShell()
        {
            InitializeComponent();
            MainPageContent = HomeShell;
            AddExpenseContent = AddExpenseShell;

            var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData), "*.expenses.txt");
            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    string[] lines = File.ReadAllLines(file);
                    if (lines[0] != "")
                    {
                        var category = lines[2];

                        string output = Regex.Match(lines[1], @"\d+").Value;
                        var amountSpent = int.Parse(output);
                        TotalExpenses = TotalExpenses + amountSpent;
                        ExpensePage.UpdateCategoryWiseExpenses(category,amountSpent,true);
                    }
                }
            }
                    //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
                    //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
                }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
