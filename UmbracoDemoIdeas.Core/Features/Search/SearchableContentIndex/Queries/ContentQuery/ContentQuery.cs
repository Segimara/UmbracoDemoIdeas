using Examine.Lucene.Providers;
using Examine.Search;
using Umbraco.Cms.Web.Common.PublishedModels;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Constants;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Extentions;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Queries.Abstractions;

namespace UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Queries.ContentQuery
{
    public class ContentQuery : BaseQueryBuilder
    {
        public ContentQuery(BaseLuceneSearcher searcher) : base(searcher)
        {

        }

        public IBooleanOperation BuildFilter(string searchTerm)
        {
            var searchebleFields = new List<string> {
                SearchFieldConstants.Name,
                SearchFieldConstants.Description,
                SearchFieldConstants.CategoryNames,
                SearchFieldConstants.Content,
                SearchFieldConstants.BrandNames,
                SearchFieldConstants.ProductCharacteristicNames
            };

            Filter = Query.NodeTypeAliases(
                ProductPage.ModelTypeAlias
                );

            Filter = GetBaseLuceneQuery(searchTerm, Filter.And(), searchebleFields);

            return Filter;
        }
    }
}