using Examine;
using Examine.Lucene.Providers;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Constants;

namespace UmbracoDemoIdeas.Core.Features.Search.Infrastructure;
internal class ExamineSearcherAccessor
{
    private readonly IExamineManager _examineManager;

    public ExamineSearcherAccessor(IExamineManager examineManager)
    {
        _examineManager = examineManager;
    }

    public BaseLuceneSearcher GetSearchableContentIndexSercher()
    {
        if (!_examineManager.TryGetIndex(IndexType.SearchableContentIndex, out var index))
        {
            throw new InvalidOperationException($"No index found by name {IndexType.SearchableContentIndex}");
        }
        return (BaseLuceneSearcher)index.Searcher!;
    }
}
