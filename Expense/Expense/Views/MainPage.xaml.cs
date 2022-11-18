using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Expense.Models;
using System.IO;

namespace Expense.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var expenses = new List<BudgetExpense>();
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData), "*.expenses.txt");
            foreach (var file in files)
            {
                var expense = new BudgetExpense
                {
                    DatePurchased = File.GetCreationTime(file),
                    FileName = file,
                    Name = File.ReadLines(file).ElementAt(0),
                    Amount = Convert.ToInt32(File.ReadLines(file).ElementAt(1)),
                    ExpenseCategory = File.ReadLines(file).ElementAt(2)
                };
                expenses.Add(expense);
            }
            ExpenseListView.ItemsSource = expenses;
        }

        private async void ExpenseListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new ExpensePage
            {
                BindingContext = (BudgetExpense)e.SelectedItem
            });
        }
    }
}