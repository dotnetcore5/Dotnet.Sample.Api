using Xero.Demo.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Xero.Demo.Api.Datastore
{
    //dotnet ef migrations add InitialCreate -p Api\Xero.Demo.Api.Domain\Xero.Demo.Api.Domain.csproj -s Api.Test\Xero.Demo.Api.Test\Xero.Demo.Api.Tests.csproj
    //dotnet ef database update -p Api\Xero.Demo.Api.Domain\Xero.Demo.Api.Domain.csproj -s Api.Test\Xero.Demo.Api.Tests\Xero.Demo.Api.Tests.csproj
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