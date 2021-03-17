using Microsoft.EntityFrameworkCore;

namespace Dotnet.Sample.Infrastructure.Languages
{
    public class LocalizationDbContext : DbContext
    {
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("LocalizationDb");
        }
    }
}