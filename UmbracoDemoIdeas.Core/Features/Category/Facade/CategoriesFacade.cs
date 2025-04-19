using UmbracoDemoIdeas.Core.Features.Category.Factory;
using UmbracoDemoIdeas.Core.Features.Category.Models;
using UmbracoDemoIdeas.Core.Infrastructure.Providers;

namespace UmbracoDemoIdeas.Core.Features.Category.Facade;

public class CategoriesFacade(UmbracoContentProvider umbracoContentProvider, CategoryModelsFactory categoryModelsFactory)
{
    public async Task<IEnumerable<CategoryResponseModel>> GetCategoriesAsync()
    {
        var categories = umbracoContentProvider.CategoryPages();

        return categoryModelsFactory.CreateCategoryModels(categories);
    }
}