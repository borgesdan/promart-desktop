using Microsoft.EntityFrameworkCore;
using Promart.Database.Entities;

namespace Promart.Database.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<StudentRelationship> StudentRelationships { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}