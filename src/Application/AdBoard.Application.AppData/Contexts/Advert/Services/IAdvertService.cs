using AdBoard.Contracts.Advert;

namespace AdBoard.Application.AppData.Contexts.Advert.Services;

using Advert = AdBoard.Domain.Advert.Advert;

public interface IAdvertService
{
    /// <summary>
    /// Получить список объявлений
    /// </summary>
    /// <param name="request">Запрос на фильтрацию и поиск</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список объявлений <see cref="ShortAdvertDto"/></returns>
    Task<ShortAdvertDto[]> GetAll(AdvertSearchRequestDto? request, CancellationToken cancellationToken);

    /// <summary>
    /// Получить объявление по ID
    /// </summary>
    /// <param name="id">Идентификатор объявления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Объявление <see cref="AdvertDto"/> по идентификатору</returns>
    Task<AdvertDto?> GetById(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Создать объявление по модели
    /// </summary>
    /// <param name="dto">Модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданное объявление <see cref="ShortAdvertDto"/></returns>
    Task<ShortAdvertDto> Add(CreateAdvertDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновить объявление по модели
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="dto">Модель с обновлёнными данными</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновлённое объявление <see cref="ShortAdvertDto"/></returns>
    Task<ShortAdvertDto> Update(Guid id, UpdateAdvertDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить объявление по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}