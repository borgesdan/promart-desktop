using Microsoft.EntityFrameworkCore;
using Promart.Database.Context;
using System.Windows;

namespace Promart
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    { 
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            var appDbContext = AppDbContextFactory.Create();

            var canConnect = await appDbContext.Database.CanConnectAsync();

            if (!canConnect)
                await appDbContext.Database.MigrateAsync();
        }
    }
}
