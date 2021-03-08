using Microsoft.EntityFrameworkCore;

namespace Xero.Demo.Api.Domain.Languages
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