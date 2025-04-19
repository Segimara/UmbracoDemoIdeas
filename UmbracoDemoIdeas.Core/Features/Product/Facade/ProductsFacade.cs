using UmbracoDemoIdeas.Core.Features.Product.Factory;
using UmbracoDemoIdeas.Core.Infrastructure.Providers;

namespace UmbracoDemoIdeas.Core.Features.Product.Facade;
public class ProductsFacade(UmbracoContentProvider umbracoContentProvider,
    ProductModelsFactory productModelsFactory)
{
    public async Task<IEnumerable<ProductResponseModel>> GetProductsAsync(Guid? categoryId)
    {
        var products = umbracoContentProvider.ProductPages(categoryId);

        return productModelsFactory.CreateProductModels(products);
    }
}
