namespace AdBoard.Contracts.Category;

/// <summary>
/// Данные о категории
/// </summary>
public class CategoryDto
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
    /// Дочерние категории
    /// </summary>
    public virtual IEnumerable<ShortCategoryDto> Childs { get; set; }
}