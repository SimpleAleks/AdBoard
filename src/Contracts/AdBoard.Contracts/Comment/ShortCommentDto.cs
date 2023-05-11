using AdBoard.Contracts.User;

namespace AdBoard.Contracts.Comment;

/// <summary>
/// Сокращенная информация о комментарии
/// </summary>
public class ShortCommentDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid? Id { get; set; }
    
    /// <summary>
    /// Содержимое комментария
    /// </summary>
    public string Text { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime Created { get; set; }
    
    /// <summary>
    /// Идентификатор родительского комментария
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// Пользователь, написавший комментарий
    /// </summary>
    public ShortUserDto User { get; set; }
}