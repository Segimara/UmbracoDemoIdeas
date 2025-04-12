using Examine.Lucene.Providers;
using Examine.Lucene.Search;
using Examine.Search;

namespace UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Extentions
{
    public static class ExamineExtensions
    {
        public static IQuery CreateContentQuery(this BaseLuceneSearcher searcher)
        {
            return searcher.CreateQuery("content",
            BooleanOperation.And,
            searcher.LuceneAnalyzer,
            new LuceneSearchOptions
            {
                AllowLeadingWildcard = true,
            });
        }
        public static IBooleanOperation NodeTypeAliases(this IQuery query, params string[] nodeTypeAliases)
        {
            return query.GroupedOr(new List<string> { "__NodeTypeAlias" }, nodeTypeAliases);
        }
    }
}