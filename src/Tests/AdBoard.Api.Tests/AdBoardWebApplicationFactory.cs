using AdBoard.Infrastructure.DataAccess;
using AdBoard.Infrastructure.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdBoard.Api.Tests;

public class AdBoardWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor =
                services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextOptionsConfigurator<AdBoardDbContext>));

            services.Remove(descriptor!);

            services.AddSingleton<IDbContextOptionsConfigurator<AdBoardDbContext>, TestDbContextConfiguration>();

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AdBoardDbContext>();

            db.Database.EnsureCreated();
            DataSeedHelp.AddDataToDb(db);
        });
    }
}