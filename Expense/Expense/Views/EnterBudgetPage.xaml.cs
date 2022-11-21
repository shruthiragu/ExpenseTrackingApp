using System;
using System.Collections.Generic;
using System.IO;
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

        protected override void OnAppearing()
        {
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "monthyBudget.txt")))
            {
                var existingMonthlyBudget = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "monthyBudget.txt"));
                EnterBudget.Text = existingMonthlyBudget;
                AppShell.BudgetAmt = Int32.Parse(EnterBudget.Text);
            }            
        }

        private void MonthlyBudgetSubmit_Clicked(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "monthyBudget.txt"))){
                //Existing monthly budget
                var existingMonthlyBudget = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "monthyBudget.txt"));               
                AppShell.BudgetAmt = Int32.Parse(EnterBudget.Text);
                var monthlyBudgetFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "monthyBudget.txt");
                File.WriteAllText(monthlyBudgetFile, EnterBudget.Text);
            } else {
                //New monthly budget
                var monthlyBudgetFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"monthyBudget.txt");
                File.WriteAllText(monthlyBudgetFile, EnterBudget.Text);
                AppShell.BudgetAmt = Int32.Parse(EnterBudget.Text);
            }



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