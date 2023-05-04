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
    /// Объявления
    /// </summary>
    public virtual IEnumerable<Advert.Advert> Adverts { get; set; }
}