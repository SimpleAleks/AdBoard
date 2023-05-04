using AdBoard.Infrastructure.DataAccess;
using AdBoard.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AdBoard.Api.Tests;

public class TestDbContextConfiguration : IDbContextOptionsConfigurator<AdBoardDbContext>
{
    private const string DatabaseName = "AdBoard";
    private readonly ILoggerFactory _loggerFactory;

    public TestDbContextConfiguration(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    public void Configure(DbContextOptionsBuilder<AdBoardDbContext> options)
    {
        options.UseInMemoryDatabase(DatabaseName);
        options.UseLoggerFactory(_loggerFactory);
        options.EnableSensitiveDataLogging();
    }
}