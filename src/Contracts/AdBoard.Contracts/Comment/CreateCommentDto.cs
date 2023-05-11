using System.ComponentModel.DataAnnotations;

namespace AdBoard.Contracts.Comment;

/// <summary>
/// Модель создания комментария
/// </summary>
public class CreateCommentDto
{
    /// <summary>
    /// Содержимое комментария
    /// </summary>
    [Required]
    [StringLength(1024, ErrorMessage = "{0} length can't be more than {1}.")]
    public string Text { get; set; }

    /// <summary>
    /// Родитель (если комментарий является ответом на другой комментарий)
    /// </summary>
    public Guid? ParentId { get; set; }
}