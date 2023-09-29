using AdBoard.Contracts.Category;

namespace AdBoard.Application.AppData.Contexts.Category.Repositories;

using Category = AdBoard.Domain.Category.Category;

/// <summary>
/// Умный репозиторий для <see cref="Category"/>
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Получить список категорий
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список категорий <see cref="ShortCategoryDto"/></returns>
    Task<ShortCategoryDto[]> GetAll(CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить категорию по ID
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Категория <see cref="CategoryDto"/> по идентификатору</returns>
    Task<CategoryDto?> GetById(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавить категорию по модели
    /// </summary>
    /// <param name="category">Модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданная категория <see cref="ShortCategoryDto"/></returns>
    Task<ShortCategoryDto> Add(Category category, CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет коллекцию категорий.
    /// </summary>
    /// <param name="entities">Коллекция категорий.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task AddRange(IEnumerable<Category> entities, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновить категорию по модели
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="category">Модель с обновлёнными данными</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновлённая категория <see cref="ShortCategoryDto"/></returns>
    Task<ShortCategoryDto> Update(Guid id, Category category, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить категорию по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}