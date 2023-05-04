using AdBoard.Contracts.Image;

namespace AdBoard.Application.AppData.Contexts.Image.Repositories;

using Image = AdBoard.Domain.Image.Image;

/// <summary>
/// Репозиторий для работы с изображениями <see cref="Image"/>
/// </summary>
public interface IImagesRepository
{
    /// <summary>
    /// Получить список изображений
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список изображений <see cref="ShortImageDto"/></returns>
    Task<ShortImageDto[]> GetAll(CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить изображение по ID
    /// </summary>
    /// <param name="id">Идентификатор изображения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>изображение <see cref="ImageDto"/> по идентификатору</returns>
    Task<ImageDto?> GetById(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавить изображение по модели
    /// </summary>
    /// <param name="image">Модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданное изображение <see cref="ShortImageDto"/></returns>
    Task<ShortImageDto> Add(Image image, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновить изображение по модели
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="image">Модель с обновлёнными данными</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновлённое изображение <see cref="ShortImageDto"/></returns>
    Task<ShortImageDto> Update(Guid id, Image image, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить изображение по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}