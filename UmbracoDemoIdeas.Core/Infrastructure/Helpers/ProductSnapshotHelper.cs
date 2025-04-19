using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Commerce.Core.Models;
using Umbraco.Commerce.Core.Services;
using UmbracoDemoIdeas.Core.Infrastructure.Providers;

namespace UmbracoDemoIdeas.Core.Infrastructure.Helpers;
public class ProductSnapshotHelper(UmbracoContentProvider umbracoContentProvider,
    IProductService productService)
{

    public IProductSnapshot GetProductSnapshot(IPublishedContent product)
    {
        var store = umbracoContentProvider.GetDefaultStore();

        if (store == null)
        {
            return null;
        }

        return productService.GetProduct(store.Id, product.Key.ToString(), Thread.CurrentThread.CurrentCulture.Name);
    }
}
