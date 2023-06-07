namespace AdBoard.Contracts.Category;

/// <summary>
/// Краткое описание категории
/// </summary>
public class ShortCategoryDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Родительская категория
    /// </summary>
    public Guid ParentId { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
}