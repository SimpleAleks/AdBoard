namespace AdBoard.Domain.Image;

/// <summary>
/// Картинка в объявлении
/// </summary>
public class Image
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid? Id { get; set; }
    
    /// <summary>
    /// Идентификатор объявления
    /// </summary>
    public Guid? AdvertId { get; set; }
    
    /// <summary>
    /// Навигационное свойство на <see cref="Advert"/>
    /// </summary>
    public virtual Advert.Advert? Advert { get; set; }
    
    /// <summary>
    /// Массив байтов, содержащий изображение
    /// </summary>
    public byte[] Content { get; set; }
}