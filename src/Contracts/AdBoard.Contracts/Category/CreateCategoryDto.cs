using System.ComponentModel.DataAnnotations;

namespace AdBoard.Contracts.Category;

/// <summary>
/// Данные для создания категории
/// </summary>
public class CreateCategoryDto
{
    /// <summary>
    /// Название
    /// </summary>
    [Required]
    [StringLength(64, ErrorMessage = "{0} length can't be more than {1} and less than {2}.", MinimumLength = 3)]
    public string Name { get; set; }
    
    /// <summary>
    /// Id категории выше по иерархии (если есть)
    /// </summary>
    public Guid? ParentId { get; set; }
}