using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Expense.Views;
using System.IO;

namespace Expense
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public ShellContent MainPageContent;
        public ShellContent AddExpenseContent;
        public static int BudgetAmt;
        public static int TotalExpenses;
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
                        TotalExpenses = TotalExpenses + int.Parse(lines[1]);
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
