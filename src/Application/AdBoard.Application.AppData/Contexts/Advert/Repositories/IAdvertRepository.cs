using System.Linq.Expressions;
using AdBoard.Contracts.Advert;

namespace AdBoard.Application.AppData.Contexts.Advert.Repositories;

/// <summary>
/// Умный репозиторий для <see cref="Domain.Advert.Advert"/>
/// </summary>
public interface IAdvertRepository
{
    /// <summary>
    /// Получить список объявлений
    /// </summary>
    /// <param name="request">Запрос на фильтрацию и поиск</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список объявлений <see cref="ShortAdvertDto"/></returns>
    Task<ShortAdvertDto[]> GetAll(AdvertSearchRequestDto? request, CancellationToken cancellationToken);

    /// <summary>
    /// Получить объявления удовлетворяющие предикату
    /// </summary>
    /// <param name="search">Поисковая строка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список объявлений <see cref="ShortAdvertDto"/></returns>
    Task<ShortAdvertDto[]> GetAllBySearch(string search, CancellationToken cancellationToken);

    /// <summary>
    /// Получить объявление по ID
    /// </summary>
    /// <param name="id">Идентификатор объявления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Объявление <see cref="AdvertDto"/> по идентификатору</returns>
    Task<AdvertDto?> GetById(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавить объявление по модели
    /// </summary>
    /// <param name="advert">Модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданное объявление <see cref="ShortAdvertDto"/></returns>
    Task<ShortAdvertDto> Add(Domain.Advert.Advert advert, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновить объявление по модели
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="advert">Модель с обновлёнными данными</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновлённое объявление <see cref="ShortAdvertDto"/></returns>
    Task<ShortAdvertDto> Update(Guid id, Domain.Advert.Advert advert, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить объявление по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}