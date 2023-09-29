using AdBoard.Contracts.Category;

namespace AdBoard.Application.AppData.Contexts.Category.Services;

using Category = AdBoard.Domain.Category.Category;

/// <summary>
/// Сервис для работы с категориями <see cref="Category"/>
/// </summary>
public interface ICategoryService
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
    /// Создать категорию по модели
    /// </summary>
    /// <param name="dto">Модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданная категория <see cref="ShortCategoryDto"/></returns>
    Task<ShortCategoryDto> Add(CreateCategoryDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновить категорию по модели
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="dto">Модель с обновлёнными данными</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновлённая категория <see cref="ShortCategoryDto"/></returns>
    Task<ShortCategoryDto> Update(Guid id, UpdateCategoryDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить категорию по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Удалена ли категория</returns>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Импортирует категории из JSON.
    /// </summary>
    /// <param name="importCategories">Категории.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task ImportAsync(IEnumerable<ImportCategoryDto> importCategories, CancellationToken cancellationToken);
}