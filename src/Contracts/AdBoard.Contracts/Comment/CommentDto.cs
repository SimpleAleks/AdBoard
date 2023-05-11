using AdBoard.Contracts.Advert;
using AdBoard.Contracts.User;

namespace AdBoard.Contracts.Comment;

/// <summary>
/// Информация о комментарии
/// </summary>
public class CommentDto
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
    /// Родитель (если комментарий является ответом на другой комментарий)
    /// </summary>
    public Guid? ParentId { get; set; }
    
    /// <summary>
    /// Объявление, к которому относится комментарий
    /// </summary>
    public ShortAdvertDto Advert { get; set; }
    
    /// <summary>
    /// Пользователь, написавший комментарий
    /// </summary>
    public ShortUserDto User { get; set; }
}