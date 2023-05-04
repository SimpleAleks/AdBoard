namespace AdBoard.Contracts.Image;

/// <summary>
/// Информация об изображении
/// </summary>
public class ImageDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор объявления
    /// </summary>
    public Guid AdvertId { get; set; }

    /// <summary>
    /// Массив байтов, содержащий изображение
    /// </summary>
    public byte[] Content { get; set; }
}