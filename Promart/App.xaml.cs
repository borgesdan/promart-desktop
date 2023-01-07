using Promart.Database.Context;
using System.Windows;

namespace Promart
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly AppDbContextFactory _factory = new AppDbContextFactory();
        public static AppDbContext AppDbContext => _factory.CreateDbContext(null);
    }
}
