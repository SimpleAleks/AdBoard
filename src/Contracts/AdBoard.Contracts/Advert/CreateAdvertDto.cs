using System.ComponentModel.DataAnnotations;
using AdBoard.Contracts.Attributes;
using Microsoft.AspNetCore.Http;

namespace AdBoard.Contracts.Advert;

/// <summary>
/// Создание объявления
/// </summary>
public class CreateAdvertDto
{
    /// <summary>
    /// Название.
    /// </summary>
    [Required]
    [StringLength(256, ErrorMessage = "{0} length can't be more than {1} and less than {2}.", MinimumLength = 4)]
    public string Name { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    [StringLength(256, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? Description { get; set; }

    /// <summary>
    /// Изображения объявлений
    /// </summary>
    [DataType(DataType.Upload)]
    [FilesExtension(new []{".jpeg", ".png", ".jpg"})]
    public IEnumerable<IFormFile>? Images { get; set; }

    /// <summary>
    /// Цена.
    /// </summary>
    [Required]
    [Range(0, double.MaxValue)]
    public decimal? Cost { get; set; }
    
    /// <summary>
    /// Email.
    /// </summary>
    [EmailAddress]
    public string? Email { get; set; }
    
    /// <summary>
    /// Телефон.
    /// </summary>
    [Required]
    [Phone]
    public string Phone { get; set; }
    
    /// <summary>
    /// Геолокация объявления
    /// </summary>
    [Required]
    [StringLength(256, ErrorMessage = "{0} length can't be more than {1} and less than {2}.", MinimumLength = 1)]
    public string Location { get; set; }

    /// <summary>
    /// Id категории.
    /// </summary>
    [Required]
    public Guid? CategoryId { get; set; }
}