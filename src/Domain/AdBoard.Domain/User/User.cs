namespace AdBoard.Domain.User;
/// <summary>
/// Пользователь
/// </summary>
public class User
{
    /// <summary>
    /// ID пользователя
    /// </summary>
    public Guid? Id { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Роль
    /// </summary>
    public string Role { get; set; }
    
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Объявления
    /// </summary>
    public virtual IEnumerable<Advert.Advert> Adverts { get; set; }
    
    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime RegisteredTime { get; set; }
}