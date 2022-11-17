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
            var expenses = new List<Expense>();
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData), "*.expenses.txt");
            foreach (var in files)
            {
                var expense = new Expense
                {
                    DatePurchased = File.GetCreationTime(files),
                    FileName = file,
                    Name = "",
                    Amount = 0,
                    ExpenseCategory =
                }
            }
        }

        private async void ExpenseListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new Expense
            {
                BindingContext = (Expense)e.SelectedItem
            });
        }
    }