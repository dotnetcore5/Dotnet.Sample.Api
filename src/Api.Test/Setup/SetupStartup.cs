using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xero.Demo.Api.Datastore;
using Xero.Demo.Api.Tests.EndpointTests.UnitTests.V1.TestData;

namespace Xero.Demo.Api.Tests.Setup
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<Database>));

                services.Remove(descriptor);

                services.AddDbContext<Database>(options => options.UseInMemoryDatabase("TestDB"));
                //services.AddSingleton<IDatabase>(provider => provider.GetService<Database>());

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<Database>();
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                try
                {
                    Task.Run(async () => await InitializeDbForTests(db));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {Message}", ex.Message);
                }
            });
        }

        private static async Task InitializeDbForTests(Database db)
        {
            if (!await db.Database.EnsureDeletedAsync())
            {
                await db.Database.EnsureCreatedAsync();
                await db.Products.AddRangeAsync(SampleDataV1.Products);
                await db.SaveChangesAsync();
            }
        }
    }
}