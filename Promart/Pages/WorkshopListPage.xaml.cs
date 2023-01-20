using Microsoft.EntityFrameworkCore;
using Promart.Controls;
using Promart.Database.Context;
using Promart.Database.Entities;
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
        public WorkshopListPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using var context = AppDbContextFactory.Create();

            var workshops = await context.Workshops
                .Select(w => new WorkshopDetailData
                {
                    Workshop = w,
                    StudentCount = w.Students.Count,
                    RegisteredStudentsCount = w.Students.Where(s => s.ProjectStatus == Database.ProjectStatusType.Matriculado).Count(),
                    
                })
                .AsNoTracking()
                .ToListAsync();

            workshops.ForEach(w => MainPanel.Children.Add(new WorkshopDetailControl(w)));
        }

        public class WorkshopDetailData
        {
            public Workshop Workshop { get; set; }
            public int StudentCount { get; set; }
            public int RegisteredStudentsCount { get; set; }
        }
    }
}
