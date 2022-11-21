﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Expense.Models;
using System.IO;
using Xamarin.Forms.Shapes;

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
                if (File.Exists(file))
                {                    
                    string[] lines = File.ReadAllLines(file);
                    var expense = new BudgetExpense
                    {
                        DatePurchased = File.GetCreationTime(file),
                        FileName = file,
                        Name = lines[0],
                        Amount = int.Parse(lines[1]),
                        Category = (ExpenseCategory)Enum.Parse(typeof(ExpenseCategory), lines[2])
                    };
                    expenses.Add(expense);
                }
               
            }
            ExpenseListView.ItemsSource = expenses.OrderByDescending(t => t.DatePurchased);
        }

        private async void ExpenseListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Shell.Current.Navigation.PushModalAsync(new ExpensePage
            {
                BindingContext = (BudgetExpense)e.SelectedItem
            });
        }
    }
}