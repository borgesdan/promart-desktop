using Microsoft.EntityFrameworkCore;
using Promart.Controls;
using Promart.Database.Context;
using Promart.Database.Entities;
using Promart.Services;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for WorkshopListPage.xaml
    /// </summary>
    public partial class WorkshopListPage : Page
    {
        readonly MainWindowService _mainWindowService = new MainWindowService();

        AppDbContext context = AppDbContextFactory.Create();

        public WorkshopListPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();            

            var workshops = await context.Workshops
                .Select(w => new WorkshopDetailData
                {
                    Workshop = w,
                    StudentCount = w.Students.Count,
                    RegisteredStudentsCount = w.Students.Where(s => s.ProjectStatus == Database.ProjectStatusType.Matriculado).Count(),

                })
                .AsNoTracking()
                .ToListAsync();

            workshops.ForEach(w => {
                var control = new WorkshopDetailControl(w);
                control.MouseLeftButtonDown += (s, e) =>
                {
                    _mainWindowService.NavigateToWorkshopRegistryPage(w.Workshop.Id, w.Workshop.Name);
                };

                MainPanel.Children.Add(control);
            } );
        }

        public class WorkshopDetailData
        {
            public Workshop Workshop { get; set; }
            public int StudentCount { get; set; }
            public int RegisteredStudentsCount { get; set; }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            _mainWindowService.NavigateToWorkshopRegistryPage();
        }
    }
}
