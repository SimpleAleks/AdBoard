using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace AdBoard.Infrastructure.DataAccess;

/// <summary>
/// Основной контекст данных
/// </summary>
public class AdBoardDbContext : DbContext
{
    /// <summary>
    /// Конструктор <see cref="AdBoardDbContext"/>
    /// </summary>
    /// <param name="options">Настройки контекста</param>
    public AdBoardDbContext(DbContextOptions options) : base(options)
    {
    }
    
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i =>
            i.IsGenericType &&
            i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
    }
}