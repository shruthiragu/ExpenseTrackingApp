using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Expense.Models;
using System.IO;
using System.linq;

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
            foreach (var file in files)
            {
                var expense = new Expense
                {
                    DatePurchased = File.GetCreationTime(file),
                    FileName = file,
                    Name = File.ReadLines(file).First(),
                    Amount = File.ReadLines(file).Second(),
                    Category = file.ReadLines(file).Third()
                };
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