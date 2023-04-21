namespace AdBoard.Contracts.User;

/// <summary>
/// Данные для создания нового пользователя
/// </summary>
public class CreateUserDto
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
}