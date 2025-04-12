using Examine.Lucene.Providers;
using Examine.Search;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Constants;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Extentions;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Queries.Abstractions;
using UmbracoDemoIdeas.Core.Infrastructure.Extentions;

namespace UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Queries.ProductFilterQuery;
internal class ProductFilterQuery : BaseQueryBuilder
{
    public ProductFilterQuery(BaseLuceneSearcher searcher) : base(searcher)
    {

    }

    public IBooleanOperation BuildFilter(ProductFilterSearchTerm searchTerm)
    {
        Filter = Query.NodeTypeAliases(ProductPage.ModelTypeAlias);

        var searchebleFields = new List<string> {
            SearchFieldConstants.Name,
            SearchFieldConstants.Description,
            SearchFieldConstants.CategoryNames,
            SearchFieldConstants.Content,
            SearchFieldConstants.BrandNames,
            SearchFieldConstants.ProductCharacteristicNames
        };


        if (!searchTerm.SearchTerm.IsNullOrWhiteSpace())
        {
            Filter = GetBaseLuceneQuery(searchTerm.SearchTerm, Filter.And(), searchebleFields);
            Filter.And().Field(SearchFieldConstants.Description, searchTerm.SearchTerm?.ToString());
        }

        if (searchTerm.CategoryId != Guid.Empty)
        {
            Filter.And().Field(SearchFieldConstants.CategoryId, searchTerm.CategoryId.ToString());
        }

        return Filter;
    }

    private void AddFilter(int[]? ids, string fieldName)
    {
        if (ids.IsNotEmpty())
        {
            Filter = Filter.And().GroupedOr(new List<string> { fieldName }, ids.EmptyIfNull().Select(x => x.ToString()).ToArray());
        }
    }
}
