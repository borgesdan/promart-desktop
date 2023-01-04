using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Promart.Database.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<AppDbContext> builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer(@"Data Source=DESKTOP-P4JVSUH;Initial Catalog=PromartDesktop;Integrated Security=True;Trust Server Certificate=True");            

            return new AppDbContext(builder.Options);
        }
    }
}
