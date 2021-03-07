using Rest.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Rest.Api.Datastore
{
    //dotnet ef migrations add InitialCreate -p Api\Rest.Api.Domain\Rest.Api.Domain.csproj -s Api.Test\Rest.Api.Test\Rest.Api.Tests.csproj
    //dotnet ef database update -p Api\Rest.Api.Domain\Rest.Api.Domain.csproj -s Api.Test\Rest.Api.Tests\Rest.Api.Tests.csproj
    public interface IDatabase
    {
        DbSet<T> Set<T>() where T : class;

        Task<int> SaveAsync();
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

        public async Task<int> SaveAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}