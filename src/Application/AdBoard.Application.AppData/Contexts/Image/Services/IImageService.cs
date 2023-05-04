using AdBoard.Contracts.Image;

namespace AdBoard.Application.AppData.Contexts.Image.Services;

using Image = AdBoard.Domain.Image.Image;

/// <summary>
/// Сервис для работы с изображениями <see cref="Image"/>
/// </summary>
public interface IImageService
{
    /// <summary>
    /// Получить список изображений
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список изображений <see cref="ShortImageDto"/></returns>
    Task<ShortImageDto[]> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Получить изображения по ID
    /// </summary>
    /// <param name="id">Идентификатор изображения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Изображение <see cref="ImageDto"/> по идентификатору</returns>
    Task<ImageDto?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Создать изображение по модели
    /// </summary>
    /// <param name="dto">Модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданное изображение <see cref="ShortImageDto"/></returns>
    Task<ShortImageDto> Add(CreateImageDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновить изображение по модели
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="dto">Модель с обновлёнными данными</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновлённое изображение <see cref="ShortImageDto"/></returns>
    Task<ShortImageDto> Update(Guid id, UpdateImageDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить изображение по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Удалено ли изображение</returns>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}