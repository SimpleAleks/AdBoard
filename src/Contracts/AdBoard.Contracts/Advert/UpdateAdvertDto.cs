namespace AdBoard.Contracts.Advert;

/// <summary>
/// Обновление объявления
/// </summary>
public class UpdateAdvertDto
{
    // TODO: Убрать когда появится авторизация
    /// <summary>
    /// ID пользователя, создавшего объявление
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; }

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