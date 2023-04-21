using AdBoard.Contracts.Advert;

namespace AdBoard.Contracts.User;

/// <summary>
/// Полная информация о пользователе
/// </summary>
public class UserDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Объявления
    /// </summary>
    public IEnumerable<ShortAdvertDto> Adverts { get; set; }
}