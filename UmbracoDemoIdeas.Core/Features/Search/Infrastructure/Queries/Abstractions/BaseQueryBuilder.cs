using Examine.Lucene.Providers;
using Examine.Search;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Extentions;
using UmbracoDemoIdeas.Core.Infrastructure.Extentions;

namespace UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Queries.Abstractions
{
    public abstract class BaseQueryBuilder
    {
        private char[] termsSeparators = new[] { ' ', '-' };
        protected IQuery Query { get; set; }
        public BaseQueryBuilder(BaseLuceneSearcher searcher)
        {
            Query = searcher.CreateContentQuery();
        }

        protected IEnumerable<string> GetTermsFromSearchInput(string searchTerm)
        => searchTerm.Split(termsSeparators, StringSplitOptions.RemoveEmptyEntries);

        protected IBooleanOperation GetBaseLuceneQuery(string? searchTerm, IQuery baseQuery, List<string> searchebleFields)
        {
            var searchTermSanitized = new string(searchTerm.EmptyIfNull().Select(x => char.IsLetterOrDigit(x) ? x : ' ').ToArray());

            var terms = GetTermsFromSearchInput(searchTermSanitized);

            var luceneQuery = baseQuery.Field("__Published", "y");
            foreach (var term in terms)
            {

                luceneQuery = luceneQuery.And()
                    .GroupedOr(searchebleFields, new ExamineValue(Examineness.ComplexWildcard, $"*{term}*"),
                                                 new ExamineValue(Examineness.SimpleWildcard, $"*{term}*"),
                                                 new ExamineValue(Examineness.SimpleWildcard, $"*{term} *"),
                                                 new ExamineValue(Examineness.Escaped, $"{term}"));
            }

            return luceneQuery;
        }

        public IBooleanOperation Filter { get; protected set; } = null!;
    }
}