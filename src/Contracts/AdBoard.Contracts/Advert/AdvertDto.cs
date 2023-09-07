using AdBoard.Contracts.Category;
using AdBoard.Contracts.User;
using AdBoard.Domain.Category;
using AdBoard.Domain.Image;
using AdBoard.Domain.User;

namespace AdBoard.Contracts.Advert;

/// <summary>
/// Объявление (подробное описание)
/// </summary>
public class AdvertDto
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Пользователь, создавший объявление
    /// </summary>
    public ShortUserDto User { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Признак актуальности объявления.
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// ID изображений привязанных к объявлению
    /// </summary>
    public IEnumerable<Guid> ImagesIds { get; set; }

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
    /// Дата создания.
    /// </summary>
    public DateTime Created { get; set; }
    
    /// <summary>
    /// Категория объявления
    /// </summary>
    public ShortCategoryDto Category { get; set; }
}