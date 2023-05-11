using System.Linq.Expressions;
using AdBoard.Contracts.Comment;

namespace AdBoard.Application.AppData.Contexts.Comment.Repositories;

using Comment = AdBoard.Domain.Comment.Comment;

/// <summary>
/// Репозиторий комментариев
/// </summary>
public interface ICommentRepository
{
    /// <summary>
    /// Получить все комментарии, удовлетворяющие условию предиката
    /// </summary>
    /// <param name="predicate">Предикат</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все комментарии, удовлетворяющие условие предиката</returns>
    Task<ShortCommentDto[]> GetAllWhere(Expression<Func<Comment, bool>> predicate, CancellationToken cancellationToken);

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
    /// <param name="comment">Комментарий</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Краткая информация о созданном комментарии</returns>
    Task<ShortCommentDto> Add(Comment comment, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить комментарий
    /// </summary>
    /// <param name="comment">Комментарий</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Краткая информация о обновленном комментарии</returns>
    Task<ShortCommentDto> Update(Comment comment, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить комментарий
    /// </summary>
    /// <param name="id">Идентификатор комментария</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат операции</returns>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}