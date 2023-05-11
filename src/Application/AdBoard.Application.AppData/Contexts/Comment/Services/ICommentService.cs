using AdBoard.Contracts.Comment;

namespace AdBoard.Application.AppData.Contexts.Comment.Services;

/// <summary>
/// Сервис для работы с комментариями
/// </summary>
public interface ICommentService
{
    /// <summary>
    /// Получить все комментарии объявления
    /// </summary>
    /// <param name="advertId">Идентификатор объявления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task<ShortCommentDto[]> GetAllByAdvertId(Guid advertId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор комментария</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Комментарий <see cref="CommentDto"/></returns>
    Task<CommentDto?> GetById(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавить комментарий
    /// </summary>
    /// <param name="advertId">Идентификатор объявления</param>
    /// <param name="dto">Модель создаваемого комментария</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Краткая информация о созданном комментарии</returns>
    Task<ShortCommentDto> Create(Guid advertId, CreateCommentDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновить комментарий
    /// </summary>
    /// <param name="id">Идентификатор обновляемого комментария</param>
    /// <param name="dto">Модель комментария для обновления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Краткая информация о обновленном комментарии</returns>
    Task<ShortCommentDto> Update(Guid id, UpdateCommentDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить комментарий
    /// </summary>
    /// <param name="id">Идентификатор комментария</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат операции</returns>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}