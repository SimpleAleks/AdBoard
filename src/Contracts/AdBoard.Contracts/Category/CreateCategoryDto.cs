namespace AdBoard.Contracts.Category;

/// <summary>
/// Данные для создания категории
/// </summary>
public class CreateCategoryDto
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Id категории выше по иерархии (если есть)
    /// </summary>
    public Guid? ParentId { get; set; }
}