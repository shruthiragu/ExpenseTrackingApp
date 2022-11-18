using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Expense.Views;

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
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
