namespace AdBoard.Contracts.Advert;

/// <summary>
/// Модель запроса на поиск объявлений.
/// </summary>
public class AdvertSearchRequestDto
{
    /// <summary>
    /// Строка поиска.
    /// </summary>
    public string? Search { get; set; }
    
    /// <summary>
    /// Показывать неактивные объявления
    /// </summary>
    /// <remarks>
    /// Неактивные объявления, это объявления у которых поле IsActive
    /// равно false. Эти объявления могли быть удалены или закрыты продавцом.
    /// </remarks>
    public bool? ShowNonActive { get; set; }
    
    /// <summary>
    /// Категория
    /// </summary>
    public Guid? Category { get; set; }
}