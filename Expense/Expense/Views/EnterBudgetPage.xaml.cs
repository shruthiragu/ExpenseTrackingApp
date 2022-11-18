using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterBudgetPage : ContentPage
    {
        public EnterBudgetPage()
        {
            InitializeComponent();
        }

        private void MonthlyBudgetSubmit_Clicked(object sender, EventArgs e)
        {
            AppShell.BudgetAmt = Int32.Parse(EnterBudget.Text);
            if (Navigation.ModalStack.Count > 0)
            {
                Navigation.PopModalAsync();
            }
            else
            {
                Shell.Current.CurrentItem = (Shell.Current as AppShell).AddExpenseContent;
            }
        }
    }
}