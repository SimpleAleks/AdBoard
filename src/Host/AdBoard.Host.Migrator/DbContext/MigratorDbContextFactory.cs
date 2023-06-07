using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AdBoard.Host.Migrator.DbContext;

/// <summary>
/// Фабрика контекста для мигратора
/// </summary>
public class MigratorDbContextFactory : IDesignTimeDbContextFactory<MigratorDbContext>
{
    /// <inheritdoc/>
    public MigratorDbContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var configuration = builder.Build();
        var connectionString = configuration.GetConnectionString("PostgresAdBoardDb");

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<MigratorDbContext>();
        dbContextOptionsBuilder.UseNpgsql(connectionString);
        return new MigratorDbContext(dbContextOptionsBuilder.Options);
    }
}