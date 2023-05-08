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
    public string Name { get; set; }
    
    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Изображения объявлений
    /// </summary>
    public IEnumerable<IFormFile> Images { get; set; }

    /// <summary>
    /// Цена.
    /// </summary>
    public decimal Cost { get; set; }
    
    /// <summary>
    /// Email.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Телефон.
    /// </summary>
    public string Phone { get; set; }
    
    /// <summary>
    /// Геолокация объявления
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// Id категории.
    /// </summary>
    public Guid CategoryId { get; set; }
}