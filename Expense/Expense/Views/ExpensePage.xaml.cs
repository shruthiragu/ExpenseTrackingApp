using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                AmountText.Text = "$"+lines[1];
                var category = lines[2];
                CheckCorrespondingCategoryRadioButton(category);                
            } else
            {
                ExpenseText.Text = string.Empty;
                AmountText.Text = string.Empty;
                MiscRadioButton.IsChecked = true;
            }
        }

        private void CheckCorrespondingCategoryRadioButton(string category)
        {
            switch (category)
            {
                case "Grocery":
                    GroceryRadioButton.IsChecked = true;
                    break;
                case "Travel":
                    TravelRadioButton.IsChecked = true;
                    break;
                case "Shopping":
                    ShoppingRadioButton.IsChecked = true;
                    break;
                case "Misc":
                    MiscRadioButton.IsChecked = true;
                    break;
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {            
            var budgetExpense = (BudgetExpense)BindingContext;

            //Check for empty expense
            if (string.IsNullOrWhiteSpace(ExpenseText.Text) || string.IsNullOrWhiteSpace(AmountText.Text))
            {
                //Display error
                await DisplayAlert("Alert", "Please enter a valid expense name and amount!", "OK");
            }
            //New Expense
            else if (budgetExpense == null || string.IsNullOrEmpty(budgetExpense.FileName))
            {
                string output = Regex.Match(AmountText.Text, @"\d+").Value;
                budgetExpense = new BudgetExpense(chosenCategory);
                if (AppShell.BudgetAmt - AppShell.TotalExpenses >= int.Parse(output))
                {
                    
                    AppShell.TotalExpenses = AppShell.TotalExpenses + int.Parse(output);
                    UpdateCategoryWiseExpenses(chosenCategory, int.Parse(output), true);
                    budgetExpense.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{Path.GetRandomFileName()}.expenses.txt");
                    var fileContents = $"{ExpenseText.Text}" + "\n" + output + "\n" + chosenCategory;
                    File.WriteAllText(budgetExpense.FileName, fileContents);
                    await DisplayAlert("Message", "Your expense has been added successfully!", "OK");
                }
                else
                {
                    //Display error
                    await DisplayAlert("Alert", "You are exceeding your budget!", "OK");
                }
            }
            else //Existing Expense
            {
                var oldTotalExpense = AppShell.TotalExpenses - budgetExpense.Amount;
                string output = Regex.Match(AmountText.Text, @"\d+").Value;
                if (AppShell.BudgetAmt - oldTotalExpense >= int.Parse(output))
                {
                    
                    AppShell.TotalExpenses = oldTotalExpense + int.Parse(output);
                    UpdateCategoryWiseExpenses(chosenCategory, int.Parse(output),true);
                    var fileContents = $"{ExpenseText.Text}" + "\n" + output + "\n" + chosenCategory;
                    File.WriteAllText(budgetExpense.FileName, fileContents);
                    await DisplayAlert("Message", "Your expense has been added successfully!", "OK");
                } else
                {
                    //Display error
                    await DisplayAlert("Alert", "You are exceeding your budget!", "OK");
                }
            }     

            if (Navigation.ModalStack.Count > 0)
            {
                await Navigation.PopModalAsync();
            } else
            {
                Shell.Current.CurrentItem = (Shell.Current as AppShell).MainPageContent;
            }
        }

        public static void UpdateCategoryWiseExpenses(string category, int amountSpent, bool addExpense)
        {
            if (addExpense)
            {
                switch (category)
                {
                    case "Grocery":
                        AppShell.TotalGroceryExpenses += amountSpent; break;
                    case "Travel":
                        AppShell.TotalTravelExpenses += amountSpent; break;
                    case "Shopping":
                        AppShell.TotalShoppingExpenses += amountSpent; break;
                    case "Misc":
                        AppShell.TotalMiscExpenses += amountSpent; break;
                }
            }
            else
            {
                switch (category)
                {
                    case "Grocery":
                        AppShell.TotalGroceryExpenses -= amountSpent; break;
                    case "Travel":
                        AppShell.TotalTravelExpenses -= amountSpent; break;
                    case "Shopping":
                        AppShell.TotalShoppingExpenses -= amountSpent; break;
                    case "Misc":
                        AppShell.TotalMiscExpenses -= amountSpent; break;
                }
            }
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            var budgetExpense = (BudgetExpense)BindingContext;
            if (budgetExpense!=null && File.Exists(budgetExpense.FileName))
            {
                File.Delete(budgetExpense.FileName);
                string output = Regex.Match(AmountText.Text, @"\d+").Value;
                AppShell.TotalExpenses -= int.Parse(output);
                UpdateCategoryWiseExpenses(chosenCategory, int.Parse(output),false);
            }
            ExpenseText.Text = string.Empty;
            AmountText.Text = string.Empty;
            
            if (Navigation.ModalStack.Count > 0)
            {
                Navigation.PopModalAsync();
            }
            else
            {
                Shell.Current.CurrentItem = (Shell.Current as AppShell).MainPageContent;
            }
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton categoryRadioButton = (RadioButton)sender;
            chosenCategory = (string)categoryRadioButton.Value;
            
            /*if (categoryRadioButton.IsChecked == true)
            {
                switch (categoryRadioButton.Value)
                {
                    case "Grocery":
                        chosenCategory = "Grocery";
                        break;

                    case "ShoppingRadioButton":
                        //do something
                        break;

                    case "TravelRadioButton":
                        //do something
                        break;

                    case "MiscRadioButton":
                        //do something
                        break;
                }

            }*/

        }
    }
}