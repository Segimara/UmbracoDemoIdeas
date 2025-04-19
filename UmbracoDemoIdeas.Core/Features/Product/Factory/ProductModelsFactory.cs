using Umbraco.Cms.Web.Common.PublishedModels;
using UmbracoDemoIdeas.Core.Features.Common.Factory;
using UmbracoDemoIdeas.Core.Infrastructure.Extentions;
using UmbracoDemoIdeas.Core.Infrastructure.Providers;

namespace UmbracoDemoIdeas.Core.Features.Product.Factory;
public class ProductModelsFactory(CommerceFactory commerceFactory,
    UmbracoContentProvider umbracoContentProvider)
{
    internal IEnumerable<ProductResponseModel> CreateProductModels(IEnumerable<ProductPage>? products)
    {

        return products?.Select(x =>
        {
            var productSnapshot = umbracoContentProvider.GetProductSnapshot(x);
            return new ProductResponseModel
            {
                Id = x.Key,
                Name = x.Name,
                Description = x.Description.ToHtmlString(),
                FullDescription = x.FullDescription.ToHtmlString(),
                Price = commerceFactory.CreatePriceDto(productSnapshot),
                Image = x.PreveiwImage.ToImageDto(),
                CategoryId = x.Category?.Key ?? Guid.Empty,
                Category = x.Category?.Name,
            };
        }) ?? Enumerable.Empty<ProductResponseModel>();
    }


}
