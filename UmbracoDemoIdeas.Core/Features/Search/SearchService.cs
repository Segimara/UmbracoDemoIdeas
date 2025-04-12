using Examine.Search;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure;
using UmbracoDemoIdeas.Core.Features.Search.Models;
using UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Queries.ProductFilterQuery;

namespace UmbracoDemoIdeas.Core.Features.Search;
internal class SearchService : ISearchService
{
    private readonly ExamineSearcherAccessor _examiceSercherAccessor;
    private readonly SearchModelsFactory _searchModelsFactory;

    public SearchService(SearchModelsFactory searchModelsFactory, ExamineSearcherAccessor examiceSercherAccessor)
    {
        _searchModelsFactory = searchModelsFactory;
        _examiceSercherAccessor = examiceSercherAccessor;
    }

    public SearchResultViewModel Search(ProductFilterSearchTerm searchTerm)
    {
        if (searchTerm is null)
        {
            throw new ArgumentNullException(nameof(searchTerm));
        }

        var searcher = _examiceSercherAccessor.GetSearchableContentIndexSercher();
        var searchQuery = new ProductFilterQuery(searcher);

        var skip = (searchTerm.Page - 1) * searchTerm.PageSize;

        var results = searchQuery.BuildFilter(searchTerm).Execute(new QueryOptions(skip, searchTerm.PageSize));

        return _searchModelsFactory.GetSplitedSearchResults(results, searchTerm.Page, searchTerm.PageSize, searchTerm.SearchTerm);
    }
}
