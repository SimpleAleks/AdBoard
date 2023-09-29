namespace AdBoard.Contracts.Category;

/// <summary>
/// Модель категории для импорта из JSON.
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