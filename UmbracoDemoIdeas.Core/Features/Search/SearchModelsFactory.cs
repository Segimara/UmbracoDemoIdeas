using Examine;
using Serilog;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;
using UmbracoDemoIdeas.Core.Features.Search.Models;
using UmbracoDemoIdeas.Core.Infrastructure.Extentions;
using UmbracoDemoIdeas.Core.Infrastructure.Models;
using UmbracoDemoIdeas.Core.Infrastructure.Providers;

namespace UmbracoDemoIdeas.Core.Features.Search;
internal class SearchModelsFactory
{
    private readonly UmbracoContentProvider _umbracoContentProvider;

    public SearchModelsFactory(UmbracoContentProvider umbracoContentProvider)
    {
        _umbracoContentProvider = umbracoContentProvider;
    }

    public SearchResultViewModel GetSplitedSearchResults(ISearchResults searchResults, int page, int pageSize, string searchTerm)
    {
        var results = GetSearchResults(searchResults);
        var listOfProducts = results.EmptyIfNull().Where(r => r.Type == ProductPage.ModelTypeAlias);


        var homePage = _umbracoContentProvider.HomePage;
        var paginatedProducts = listOfProducts.GetPaginatedItems(page, pageSize);
        var paginatedResultsProducts = new PaginatedSearchResults<SearchResultItemViewModel>(paginatedProducts)
        {
        };

        return new SearchResultViewModel()
        {
            Products = paginatedResultsProducts,
        };
    }

    public IEnumerable<SearchResultItemViewModel> GetSearchResults(ISearchResults searchResults)
    {
        var searchResultsVMs = new List<SearchResultItemViewModel>();
        foreach (var result in searchResults)
        {
            switch (ConvertToPublishedContent(result))
            {
                case ProductPage productPage:
                    searchResultsVMs.Add(MapProductItem(productPage));
                    break;
                default:
                    Log.Error($"Search Results of type '{result.GetType().Name}' not recognised");
                    continue;
                    //throw new ArgumentException($"Search Results of type '{result.GetType().Name}' not recognised");
            }
        }
        return searchResultsVMs;
    }

    public SearchResultItemViewModel MapProductItem(ProductPage productPage)
    {
        return new SearchResultItemViewModel
        {
            Type = ProductPage.ModelTypeAlias,
            Name = productPage.Name,
            Url = productPage.Url(),
            CategoryLink = new LinkDto { Name = productPage.Category?.Name, Url = productPage.Category?.Url() },
            ShortDescription = productPage.Description?.ToHtmlString(),
            PreviewImage = ImageDto.Map(productPage.PreveiwImage)
        };
    }

    public IEnumerable<IPublishedContent?>? GetPublishedContentResult(IEnumerable<ISearchResult> result)
    {
        return result.Select(ConvertToPublishedContent);
    }

    public IEnumerable<T> GetTypedResult<T>(ISearchResults result) where T : class
    {
        return result.Select(x => (ConvertToPublishedContent(x) as T)!);
    }

    public IEnumerable<T> GetTypedResult<T>(IEnumerable<ISearchResult> result) where T : class
    {
        return result.Select(x => (ConvertToPublishedContent(x) as T)!);
    }

    public IPublishedContent? ConvertToPublishedContent(ISearchResult result)
    {
        switch (result["__IndexType"])
        {
            case "content":
                return _umbracoContentProvider.GetById(result.Id);
            default:
                throw new ArgumentException($"Index Type of '{result["__IndexType"]}' not recognised");
        }
    }
}
