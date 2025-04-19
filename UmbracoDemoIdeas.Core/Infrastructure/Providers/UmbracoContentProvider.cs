using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Commerce.Core.Models;
using Umbraco.Commerce.Core.Services;
using Umbraco.Extensions;
using UmbracoDemoIdeas.Core.Infrastructure.Extentions;

namespace UmbracoDemoIdeas.Core.Infrastructure.Providers;
public class UmbracoContentProvider(IUmbracoContextAccessor _umbracoContentAccessor, IProductService productService)
{
    public HomePage HomePage()
    {
        return (HomePage)GetRootNode()!;
    }
    public StoreReadOnly GetDefaultStore()
    {
        var store = HomePage().Store!;
        return store;
    }
    public IEnumerable<CategoryPage>? CategoryPages()
    {
        return GetRootNode()
            ?.FirstChild<CategoriesFolder>()
            ?.Children<CategoryPage>();
    }

    public IEnumerable<ProductPage>? ProductPages()
    {
        return GetRootNode()
            ?.FirstChild<ProductsFolder>()
            ?.Children<ProductPage>();
    }
    public IEnumerable<ProductPage>? ProductPages(Guid? categoryKey)
    {
        return GetRootNode()
            ?.FirstChild<ProductsFolder>()
            ?.Children<ProductPage>()
            .EmptyIfNull()
            .Where(p => categoryKey is null || p.Category?.Key == categoryKey);
    }
    public IEnumerable<ProductPage>? ProductPages(CategoryPage category)
    {
        return ProductPages(category.Key);
    }



    public IPublishedContent? GetRootNode()
    {
        var umbracoContext = GetUmbracoContext();

        var content = umbracoContext?.PublishedRequest?.PublishedContent?.Root()
            ?? umbracoContext?.Content?.GetAtRoot().FirstOrDefault();

        return content;
    }

    private IUmbracoContext GetUmbracoContext()
    {
        _umbracoContentAccessor.TryGetUmbracoContext(out var umbracoContext);

        return umbracoContext;
    }

    public IPublishedContent? GetById(string id)
    {
        return GetUmbracoContext()?.Content?.GetById(int.Parse(id));
    }
    public IPublishedContent? GetByKey(Guid key)
    {
        return GetUmbracoContext()?.Content?.GetById(key);
    }

    public ProductPage? GetProductById(Guid productId)
    {
        return GetByKey(productId) as ProductPage;
    }

    public IProductSnapshot? GetProductSnapshot(ProductPage product)
    {
        var store = GetDefaultStore();

        if (store == null)
        {
            return null;
        }

        return productService.GetProduct(store.Id, product.Key.ToString(), Thread.CurrentThread.CurrentCulture.Name);
    }
}
