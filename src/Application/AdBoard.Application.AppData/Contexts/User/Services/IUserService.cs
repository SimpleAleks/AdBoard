using AdBoard.Contracts.Advert;
using AdBoard.Contracts.User;

namespace AdBoard.Application.AppData.Contexts.Advert.Services;

using Advert = AdBoard.Domain.Advert.Advert;

/// <summary>
/// Сервис для работы с пользователями
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Получить список пользователей
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список пользователей <see cref="ShortUserDto"/></returns>
    Task<ShortUserDto[]> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Получить пользователя по ID
    /// </summary>
    /// <param name="id">Идентификатор пользоателя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь <see cref="UserDto"/> по идентификатору</returns>
    Task<UserDto?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получить текущего пользователя
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Текущий пользователь</returns>
    public Task<UserDto> GetCurrent(CancellationToken cancellationToken);

    /// <summary>
    /// Создать пользователя по модели
    /// </summary>
    /// <param name="dto">Модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданный пользователь <see cref="ShortUserDto"/></returns>
    Task<ShortUserDto> Add(CreateUserDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновить пользователя по модели
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="dto">Модель с обновлёнными данными</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновлённый пользователь <see cref="ShortUserDto"/></returns>
    Task<ShortUserDto> Update(Guid id, UpdateUserDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Удален ли пользователь</returns>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}