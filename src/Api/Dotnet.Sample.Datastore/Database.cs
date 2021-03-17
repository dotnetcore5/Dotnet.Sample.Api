using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Dotnet.Sample.Domain.Models;

namespace Dotnet.Sample.Datastore
{
    public interface IDatabase
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductOption> ProductOptions { get; set; }

        DbSet<T> Set<T>() where T : class;

        Task<int> SaveChangesAsync();
    }

    public class Database : DbContext, IDatabase
    {
        public Database(DbContextOptions options) : base(options)
        {
        }

        public override DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}