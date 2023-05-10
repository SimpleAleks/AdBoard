using System.ComponentModel.DataAnnotations;

namespace AdBoard.Contracts.User;

/// <summary>
/// Информация о пользователе для аутентификации
/// </summary>
public class LoginUserDto
{
    /// <summary>
    /// Логин
    /// </summary>
    [Required]
    [StringLength(64, ErrorMessage = "{0} length can't be more than {1} and less than {2}.", MinimumLength = 4)]
    public string Login { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    [StringLength(50, ErrorMessage = "{0} length can't be more than {1} and less than {2}.", MinimumLength = 8)]
    public string Password { get; set; }
}