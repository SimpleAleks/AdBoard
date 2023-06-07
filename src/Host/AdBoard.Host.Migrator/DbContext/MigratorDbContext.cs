using AdBoard.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace AdBoard.Host.Migrator.DbContext;

/// <summary>
/// Контекст для мигратора
/// </summary>
public class MigratorDbContext : AdBoardDbContext
{
    public MigratorDbContext(DbContextOptions options) : base(options)
    {
    }
}