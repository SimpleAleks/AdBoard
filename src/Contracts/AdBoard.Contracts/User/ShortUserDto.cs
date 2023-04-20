namespace AdBoard.Contracts.User;

/// <summary>
/// Краткая информация о пользователе
/// </summary>
public class ShortUserDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
}