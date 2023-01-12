using Microsoft.EntityFrameworkCore;
using Promart.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Promart.Controls;

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
            using var context = App.AppDbContext;

            var workshops = await context.Workshops.ToListAsync();
            workshops.ForEach(w => MainPanel.Children.Add(new WorkshopDataControl(w)));
        }
    }
}
