using AdBoard.Contracts.Category;

namespace AdBoard.Application.AppData.Contexts.Category.Services.Helpers;

public static class CategoriesHelper
{
    public static IEnumerable<Domain.Category.Category> MapImportCategoriesToDomain(IEnumerable<ImportCategoryDto> importCategories)
    {
        var parentCategories = importCategories.Where(x => x.ParentId is null);
        foreach (var parentCategory in parentCategories)
        {
            var parent = new Domain.Category.Category()
            {
                Name = parentCategory.Name
            };
            
            
        }
    }

    private static Domain.Category.Category FillCategoryRecursion(int topId, Domain.Category.Category category, IEnumerable<ImportCategoryDto> importCategories)
    {
        var childs = importCategories
            .Where(x => x.ParentId == topId);

        if (!childs.Any())
        {
            return new Domain.Category.Category()
            {
                Name = importCategories.First(x => x.Id == topId).Name
            };
        }
        else
        {
            return new Domain.Category.Category()
            {
                Name = importCategories.First(x => x.Id == topId).Name,
                Childs = childs.Select(x => FillCategoryRecursion(x.Id, ))
            }
        }
    }
}