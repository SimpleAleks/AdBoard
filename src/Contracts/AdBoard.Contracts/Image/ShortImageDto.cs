namespace AdBoard.Contracts.Image;

/// <summary>
/// Краткое описание изображения
/// </summary>
public class ShortImageDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Массив байтов, содержащий изображение
    /// </summary>
    public byte[] Content { get; set; }
}