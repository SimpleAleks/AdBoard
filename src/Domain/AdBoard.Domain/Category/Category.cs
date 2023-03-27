namespace AdBoard.Domain.Category;

/// <summary>
/// Категория.
/// </summary>
public class Category
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Id категории выше по иерархии.
    /// </summary>
    public Guid ParentId { get; set; }
    
    /// <summary>
    /// Навигационное свойство на родительскую категорию
    /// </summary>
    public virtual Category Parent { get; set; }
    
    /// <summary>
    /// Дочерние категории
    /// </summary>
    public virtual IEnumerable<Category> Childs { get; set; }

    /// <summary>
    /// Навигационное поле на объявления.
    /// </summary>
    public virtual IEnumerable<Advert.Advert> Adverts { get; set; }
}