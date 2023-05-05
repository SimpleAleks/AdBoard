namespace AdBoard.Contracts.User;

/// <summary>
/// Информация о пользователе для аутентификации
/// </summary>
public class LoginUserDto
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
}