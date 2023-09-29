namespace AdBoard.Contracts.Category;

/// <summary>
/// Инициализирует экземпляр <see cref="ImportCategoryDto"/>.
/// </summary>
public class ImportCategoryDto
{
    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Наименование категории.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Идентификатор родительской категории.
    /// </summary>
    public int? ParentId { get; set; }
}