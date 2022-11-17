using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expense.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensePage : ContentPage
    {

        public string chosenCategory;
        public ExpensePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
           
            var expense = (BudgetExpense)BindingContext;
           

            if ((expense!=null) && (!string.IsNullOrEmpty(expense.FileName))){
                string[] lines = File.ReadAllLines(expense.FileName);
                ExpenseText.Text = lines[0];
            }
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            var budgetExpense = (BudgetExpense)BindingContext;
            if (budgetExpense == null || string.IsNullOrEmpty(budgetExpense.FileName))
            {
                budgetExpense = new BudgetExpense();
                budgetExpense.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{Path.GetRandomFileName()}.{chosenCategory}.expenses.txt");
                
                
            }
            var fileContents = $"{ExpenseText.Text}" + "\n" + AmountText.Text + "\n" + chosenCategory;
            File.WriteAllText(budgetExpense.FileName, fileContents);

            if (Navigation.ModalStack.Count > 0)
            {
                Navigation.PopModalAsync();
            } else
            {
                Shell.Current.CurrentItem = (Shell.Current as AppShell).MainPageContent;
            }
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            var budgetExpense = (BudgetExpense)BindingContext;
            if (File.Exists(budgetExpense.FileName))
            {
                File.Delete(budgetExpense.FileName);
            }
            ExpenseText.Text = string.Empty;
            Navigation.PopModalAsync();
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton categoryRadioButton = sender as RadioButton;
            chosenCategory = (string)categoryRadioButton.Content;
        }
    }
}