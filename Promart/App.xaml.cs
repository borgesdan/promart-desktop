using Microsoft.EntityFrameworkCore;
using Promart.Core;
using Promart.Database.Context;
using System;
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
            try
            {
                var appDbContext = AppDbContextFactory.Create();

                var canConnect = await appDbContext.Database.CanConnectAsync();

                if (!canConnect)
                    await appDbContext.Database.MigrateAsync();
            }
            catch(Exception ex)
            {
                Error.ShowDatabaseError("Ocorreu um erro ao verificar a disponibilidade do banco de dados.", ex);
            }
        }
    }
}
