using AdBoard.Host.Migrator.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AdBoard.Host.Migrator;

/// <summary>
/// Методы расширения для добавления сервисов
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление сервисов
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="configuration">Конфигурация</param>
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbConnections(configuration);
        return services;
    }

    /// <summary>
    /// Конфигурация подключения к БД
    /// </summary>
    private static IServiceCollection ConfigureDbConnections(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresAdBoardDb");
        if (connectionString is null) throw new ArgumentNullException(nameof(connectionString));
        services.AddDbContext<MigratorDbContext>(options => options.UseNpgsql(connectionString));
        return services;
    }
}