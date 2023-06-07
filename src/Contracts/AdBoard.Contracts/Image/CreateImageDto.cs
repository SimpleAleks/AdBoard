using System.ComponentModel.DataAnnotations;
using AdBoard.Contracts.Attributes;
using Microsoft.AspNetCore.Http;

namespace AdBoard.Contracts.Image;

/// <summary>
/// Информация для создания изображения
/// </summary>
public class CreateImageDto
{
    /// <summary>
    /// Идентификатор объявления
    /// </summary>
    [Required]
    public Guid? AdvertId { get; set; }

    /// <summary>
    /// Массив байтов, содержащий изображение
    /// </summary>
    [Required]
    [FileExtension(new []{".jpeg", ".png", ".jpg"})]
    public IFormFile Content { get; set; }
}