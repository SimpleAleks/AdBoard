using AdBoard.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AdBoard.Infrastructure.DataAccess;

/// <summary>
/// Конфигурация <see cref="AdBoardDbContext"/> контекста 
/// </summary>
public class AdBoardDbContextConfiguration : IDbContextOptionsConfigurator<AdBoardDbContext>
{
    private const string PostgresConnectionStringName = "PostgresAdBoardDb";
    
    private readonly IConfiguration _configuration;
    private readonly ILoggerFactory _loggerFactory;

    /// <summary>
    /// Конструктор <see cref="AdBoardDbContextConfiguration"/>
    /// </summary>
    /// <param name="configuration">Конфигурации</param>
    /// <param name="loggerFactory">Фабрика логгеров</param>
    public AdBoardDbContextConfiguration(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        _configuration = configuration;
        _loggerFactory = loggerFactory;
    }
    
    /// <inheritdoc />
    /// <exception cref="InvalidOperationException">Строка подключения не найдена в конфигурациях</exception>
    public void Configure(DbContextOptionsBuilder<AdBoardDbContext> options)
    {
        var connectionString = _configuration.GetConnectionString(PostgresConnectionStringName);
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                $"Не найдена строка подключения с именем '{PostgresConnectionStringName}'");
        options.UseNpgsql(connectionString);
        options.UseLoggerFactory(_loggerFactory);
        options.UseLazyLoadingProxies();
    }
}