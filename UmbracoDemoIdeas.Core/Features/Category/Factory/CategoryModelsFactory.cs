using Umbraco.Cms.Web.Common.PublishedModels;
using UmbracoDemoIdeas.Core.Features.Category.Models;

namespace UmbracoDemoIdeas.Core.Features.Category.Factory;

public class CategoryModelsFactory
{
    public IEnumerable<CategoryResponseModel> CreateCategoryModels(IEnumerable<CategoryPage>? categories)
    {
        return categories?.Select(x => new CategoryResponseModel
        {
            Id = x.Key,
            Name = x.Name,
            Description = x.Description?.ToHtmlString(),
        }) ?? Enumerable.Empty<CategoryResponseModel>();
    }
}