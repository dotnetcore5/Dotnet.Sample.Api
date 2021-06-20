using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Dotnet.Sample.Domain.Models;
using Dotnet.Sample.Api.Domain.Models.Catalog;
using aspCart.Core.Domain.User;
using Dotnet.Sample.Api.Domain.Models.Sale;
using Dotnet.Portal.Domain.Statistics;
using Dotnet.Sample.Api.Dotnet.Sample.Domain.Models;

namespace Dotnet.Sample.Datastore
{
    public interface IDatabase
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<Manufacturer> Manufacturers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductCategoryMapping> ProductCategoryMappings { get; set; }
        DbSet<ProductImageMapping> ProductImageMappings { get; set; }
        DbSet<ProductManufacturerMapping> ProductManufacturerMappings { get; set; }
        DbSet<ProductSpecificationMapping> ProductSpecificationMappings { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<Specification> Specifications { get; set; }
        DbSet<OrderCount> OrderCounts { get; set; }
        DbSet<VisitorCount> VisitorCounts { get; set; }
         DbSet<ContactUsMessage> ContactUsMessage { get; set; }
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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategoryMapping> ProductCategoryMappings { get; set; }
        public DbSet<ProductImageMapping> ProductImageMappings { get; set; }
        public DbSet<ProductManufacturerMapping> ProductManufacturerMappings { get; set; }
        public DbSet<ProductSpecificationMapping> ProductSpecificationMappings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<OrderCount> OrderCounts { get; set; }
        public DbSet<VisitorCount> VisitorCounts { get; set; }
        public DbSet<ContactUsMessage> ContactUsMessage { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}