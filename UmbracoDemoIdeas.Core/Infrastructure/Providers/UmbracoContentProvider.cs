using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;
using UmbracoDemoIdeas.Core.Infrastructure.Extentions;

namespace UmbracoDemoIdeas.Core.Infrastructure.Providers;
internal class UmbracoContentProvider
{
    private readonly IUmbracoContextAccessor _umbracoContentAccessor;

    public UmbracoContentProvider(IUmbracoContextAccessor umbracoContentAccessor)
    {
        _umbracoContentAccessor = umbracoContentAccessor;
    }

    public HomePage HomePage()
    {
        return (HomePage)GetRootNode()!;
    }

    public IEnumerable<CategoryPage>? CategoryPages()
    {
        return GetRootNode()?.GetPagesOfType<CategoryPage>();
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
}
