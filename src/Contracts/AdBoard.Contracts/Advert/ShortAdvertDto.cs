namespace AdBoard.Contracts.Advert;

/// <summary>
/// Краткая информация об объявлении
/// </summary>
public class ShortAdvertDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Геолокация объявления
    /// </summary>
    public string Location { get; set; }
    
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Признак актуальности объявления.
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Изображения
    /// </summary>
    public IEnumerable<Guid> ImagesIds { get; set; }
    
    /// <summary>
    /// Цена
    /// </summary>
    public decimal Cost { get; set; }
}