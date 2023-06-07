namespace AdBoard.Domain.Comment;
using User = AdBoard.Domain.User.User;
using Advert = AdBoard.Domain.Advert.Advert;

/// <summary>
/// Комментарий объявления
/// </summary>
public class Comment
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
    public virtual Comment? Parent { get; set; }
    
    /// <summary>
    /// Идентификатор родительского комментария 
    /// </summary>
    public Guid? ParentId { get; set; }
    
    /// <summary>
    /// Дочерние комментарии
    /// </summary>
    public virtual IEnumerable<Comment> Childs { get; set; }
    
    /// <summary>
    /// Объявление, к которому относится комментарий
    /// </summary>
    public virtual Advert? Advert { get; set; }
    
    /// <summary>
    /// Идентификатор объявления
    /// </summary>
    public Guid? AdvertId { get; set; }
    
    /// <summary>
    /// Пользователь, написавший комментарий
    /// </summary>
    public virtual User? User { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid? UserId { get; set; }
}