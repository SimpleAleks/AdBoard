﻿namespace AdBoard.Contracts.Image;

/// <summary>
/// Информация для создания изображения
/// </summary>
public class CreateImageDto
{
    /// <summary>
    /// Идентификатор объявления
    /// </summary>
    public Guid? AdvertId { get; set; }

    /// <summary>
    /// Массив байтов, содержащий изображение
    /// </summary>
    public byte[] Content { get; set; }
}