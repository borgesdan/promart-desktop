using Microsoft.EntityFrameworkCore;
using Promart.Controls;
using System.Windows;
using System.Windows.Controls;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for WorkshopListPage.xaml
    /// </summary>
    public partial class WorkshopListPage : Page
    {
        public WorkshopListPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var context = App.AppDbContext;

            var workshops = await context.Workshops.ToListAsync();
            //workshops.ForEach(w => MainPanel.Children.Add(new WorkshopDataControl(w)));
        }
    }
}
