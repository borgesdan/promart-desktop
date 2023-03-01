using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Promart.Core;

namespace Promart.Database.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            return Create();
        }

        public static AppDbContext Create()
        {
            DbContextOptionsBuilder<AppDbContext> builder = new DbContextOptionsBuilder<AppDbContext>();
            var configuration = ConfigurationManager.Open();

            builder.UseSqlServer(configuration.ConnectionStrings.Default);

            return new AppDbContext(builder.Options);
        }        
    }
}
