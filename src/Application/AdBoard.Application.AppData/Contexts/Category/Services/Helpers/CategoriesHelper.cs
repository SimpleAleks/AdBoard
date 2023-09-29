using AdBoard.Contracts.Category;

namespace AdBoard.Application.AppData.Contexts.Category.Services.Helpers;

public static class CategoriesHelper
{
    public static IEnumerable<Domain.Category.Category> MapImportCategoriesToDomain(IEnumerable<ImportCategoryDto> importCategories)
    {
        var result = new List<Domain.Category.Category>();
        var parentCategories = importCategories.Where(x => x.ParentId is null);
        foreach (var parentCategory in parentCategories)
        {
            var parent = new Domain.Category.Category()
            {
                Name = parentCategory.Name
            };

            FillCategoryRecursion(parentCategory.Id, parent, importCategories);
            result.Add(parent);
        }

        return result;
    }

    private static Domain.Category.Category FillCategoryRecursion(int topId, Domain.Category.Category category, IEnumerable<ImportCategoryDto> importCategories)
    {
        var childs = importCategories
            .Where(x => x.ParentId == topId);

        if (!childs.Any())
        {
            category.Childs = new List<Domain.Category.Category>();
        }
        else
        {
            category.Childs = childs.Select(x =>
                FillCategoryRecursion(x.Id, new Domain.Category.Category() { Name = x.Name }, importCategories)).ToList();
        }

        return category;
    }
}