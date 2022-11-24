using Microcharts;
using SkiaSharp;
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
    public partial class ExpenseSummaryPage : ContentPage
    {
        public ExpenseSummaryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var groceryExpense = AppShell.TotalGroceryExpenses;
            var travelExpense = AppShell.TotalTravelExpenses;
            var shoppingExpense = AppShell.TotalShoppingExpenses;
            var miscExpense = AppShell.TotalMiscExpenses;            
            var entries = new[]
                                 {
                                     new ChartEntry(groceryExpense)
                                     {
                                         Label = "Grocery",
                                         ValueLabel = groceryExpense.ToString("C"),
                                         Color = SKColor.Parse("#2c3e50"),
                                         ValueLabelColor = SKColor.Parse("#2c3e50")
                                     },
                                     new ChartEntry(travelExpense)
                                     {
                                         Label = "Travel",
                                         ValueLabel = travelExpense.ToString("C"),
                                         Color = SKColor.Parse("#77d065"),
                                         ValueLabelColor = SKColor.Parse("#77d065")
                                     },
                                     new ChartEntry(shoppingExpense)
                                     {
                                         Label = "Shopping",
                                         ValueLabel = shoppingExpense.ToString("C"),
                                         Color = SKColor.Parse("#b455b6"),
                                         ValueLabelColor = SKColor.Parse("#b455b6")
                                     },
                                     new ChartEntry(miscExpense)
                                     {
                                         Label = "Misc",
                                         ValueLabel = miscExpense.ToString("C"),
                                         Color = SKColor.Parse("#3498db"),
                                         ValueLabelColor = SKColor.Parse("#3498db")
                                } };
            var chart = new DonutChart() { Entries = entries, LabelTextSize = 60f };
            this.chartView.Chart = chart;
        }
    }
}