using UmbracoDemoIdeas.Core.Features.Search.Models;
using UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Queries.ProductFilterQuery;

namespace UmbracoDemoIdeas.Core.Features.Search;
public interface ISearchService
{
    SearchResultViewModel Search(ProductFilterSearchTerm searchTerm);
}
