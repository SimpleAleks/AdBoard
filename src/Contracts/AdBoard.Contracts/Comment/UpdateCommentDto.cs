using System.ComponentModel.DataAnnotations;

namespace AdBoard.Contracts.Comment;

/// <summary>
/// Модель обновления комментария
/// </summary>
public class UpdateCommentDto
{
    /// <summary>
    /// Содержимое комментария
    /// </summary>
    [Required]
    [StringLength(1024, ErrorMessage = "{0} length can't be more than {1}.")]
    public string Text { get; set; }
}