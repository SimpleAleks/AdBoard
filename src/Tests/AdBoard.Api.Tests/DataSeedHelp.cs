using AdBoard.Domain.Advert;
using AdBoard.Domain.Category;
using AdBoard.Domain.Image;
using AdBoard.Domain.User;
using AdBoard.Infrastructure.DataAccess;

namespace AdBoard.Api.Tests;

/// <summary>
/// Данные для тестовой бд
/// </summary>
public class DataSeedHelp
{
    public static Guid TestUserId { get; set; }
    public static Guid TestCategoryId { get; set; }
    public static Guid TestAdvertId { get; set; }
    
    public static void AddDataToDb(AdBoardDbContext db)
    {
        var testUser = new User()
        {
            Name = "test_user_name"
        };
        db.Add(testUser);

        var testCategory = new Category()
        {
            Name = "test_category_name"
        };
        db.Add(testCategory);

        var testAdvert = new Advert()
        {
            Name = "test_advert_name",
            Description = "test_advert_description",
            Cost = (decimal)55.4,
            Location = "test_advert_location",
            Created = DateTime.UtcNow,
            Images = new Image[]
            {
                new() {Content = new byte[]{101, 001}}
            },
            Category = testCategory,
            User = testUser,
            Email = "test_advert_email",
            Phone = "test_advert_phone",
            
        };
        db.Add(testAdvert);

        db.SaveChanges();
        
        TestUserId = (Guid)testUser.Id!;
        TestCategoryId = (Guid)testCategory.Id!;
        TestAdvertId = (Guid)testAdvert.Id!;
    }
}