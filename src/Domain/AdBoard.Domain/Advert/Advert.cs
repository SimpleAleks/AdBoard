namespace AdBoard.Domain.Advert;

/// <summary>
/// Объявление.
/// </summary>
public class Advert
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid? Id { get; set; }
    
    /// <summary>
    /// ID пользователя, создавшего объявление
    /// </summary>
    public Guid? UserId { get; set; }
    
    /// <summary>
    /// Навигационное свойство на <see cref="User"/>
    /// </summary>
    public virtual User.User? User { get; set; }
    
    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Изображения объявлений
    /// </summary>
    public virtual IEnumerable<Image.Image> Images { get; set; }

    /// <summary>
    /// Цена.
    /// </summary>
    public decimal Cost { get; set; }
    
    /// <summary>
    /// Email.
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Телефон.
    /// </summary>
    public string? Phone { get; set; }
    
    /// <summary>
    /// Геолокация объявления
    /// </summary>
    public string Location { get; set; }
    
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime? Created { get; set; }
    
    /// <summary>
    /// Id категории.
    /// </summary>
    public Guid? CategoryId { get; set; }
    
    /// <summary>
    /// Навигационное свойство на категорию.
    /// </summary>
    public virtual Category.Category? Category { get; set; }
}