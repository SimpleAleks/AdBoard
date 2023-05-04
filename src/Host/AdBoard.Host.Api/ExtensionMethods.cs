using AdBoard.Application.AppData.Contexts.Advert.Repositories;
using AdBoard.Application.AppData.Contexts.Advert.Services;
using AdBoard.Application.AppData.Contexts.Category.Repositories;
using AdBoard.Application.AppData.Contexts.Category.Services;
using AdBoard.Application.AppData.Contexts.User.Services;
using AdBoard.Infrastructure.DataAccess;
using AdBoard.Infrastructure.DataAccess.Contexts.Advert.Repository;
using AdBoard.Infrastructure.DataAccess.Contexts.Category.Repository;
using AdBoard.Infrastructure.DataAccess.Contexts.User.Repository;
using AdBoard.Infrastructure.DataAccess.Interfaces;
using AdBoard.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace AdBoard.Host.Api;

/// <summary>
/// Методы расширения для класса Program
/// </summary>
public static class ExtensionMethods
{
    /// <summary>
    /// Добавить сервисы в DI
    /// </summary>
    /// <param name="serviceCollection">Сервисы программы</param>
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAdvertService, AdvertService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<ICategoryService, CategoryService>();
    }

    /// <summary>
    /// Добавить репозитории в DI
    /// </summary>
    /// <param name="serviceCollection">Сервисы программы</param>
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        serviceCollection.AddScoped<IAdvertRepository, AdvertRepository>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
    }

    /// <summary>
    /// Добавить DbContext с конфигурациями в DI
    /// </summary>
    /// <param name="serviceCollection">Сервисы программы</param>
    public static void AddDbContextWithConfigurations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDbContextOptionsConfigurator<AdBoardDbContext>, AdBoardDbContextConfiguration>();
        serviceCollection.AddDbContext<AdBoardDbContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
            ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<AdBoardDbContext>>()
                .Configure((DbContextOptionsBuilder<AdBoardDbContext>)dbOptions)));
        serviceCollection.AddScoped((Func<IServiceProvider, DbContext>) (sp => sp.GetRequiredService<AdBoardDbContext>()));
    }
}