using System.Linq.Expressions;
using AdBoard.Contracts.Advert;
using AdBoard.Contracts.User;

namespace AdBoard.Application.AppData.Contexts.Advert.Repositories;

using User = AdBoard.Domain.User.User;

/// <summary>
/// Умный репозиторий для <see cref="Domain.Advert.Advert"/>
/// </summary>
public interface IUserRepository
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
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь <see cref="UserDto"/> по идентификатору</returns>
    Task<UserDto?> GetById(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавить пользователя по модели
    /// </summary>
    /// <param name="user">Модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданный пользователь <see cref="ShortUserDto"/></returns>
    Task<ShortUserDto> Add(User user, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновить пользователя по модели
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="user">Модель с обновлёнными данными</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленный пользователь <see cref="ShortUserDto"/></returns>
    Task<ShortUserDto> Update(Guid id, User user, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Удален ли пользователь</returns>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}