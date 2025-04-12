using Examine;
using Serilog;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Cms.Web.Common.PublishedModels;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Constants;
using UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Index.Factories;

namespace UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Index
{
    internal class SearchableContentIndexComponent : IComponent
    {
        private readonly IExamineManager _examineManager;
        private readonly IUmbracoContextFactory _umbracoContextFactory;
        private readonly IIndexRebuilder _indexRebuilder;
        private readonly ProductIndexFactory _productIndexFactory;
        public SearchableContentIndexComponent(IExamineManager examineManager, IUmbracoContextFactory umbracoContextFactory, IIndexRebuilder indexRebuilder, ProductIndexFactory productIndexFactory)
        {
            _examineManager = examineManager;
            _umbracoContextFactory = umbracoContextFactory;
            _indexRebuilder = indexRebuilder;
            _productIndexFactory = productIndexFactory;
        }

        public void Initialize()
        {
            if (!_examineManager.TryGetIndex(IndexType.SearchableContentIndex, out var index))
            {
                throw new InvalidOperationException($"No index found by name {IndexType.SearchableContentIndex}");
            }

            index.TransformingIndexValues += TransformingIndexValues;

            _indexRebuilder.RebuildIndex(IndexType.SearchableContentIndex);
        }

        private void TransformingIndexValues(object? sender, IndexingItemEventArgs e)
        {
            try
            {
                if (!int.TryParse(e.ValueSet.Id, out var nodeId))
                {
                    return;
                }

                var updatedValues = e.ValueSet.Values.ToDictionary(x => x.Key, x => x.Value.ToList());

                ProcessNode(nodeId, e.ValueSet.ItemType, updatedValues);

                e.SetValues(updatedValues.ToDictionary(x => x.Key, x => (IEnumerable<object>)x.Value));
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to transform external index. Id={e.ValueSet.Id}");
            }
        }

        private void ProcessNode(int nodeId, string itemType, IDictionary<string, List<object>> updatedValues)
        {
            var context = _umbracoContextFactory.EnsureUmbracoContext().UmbracoContext.Content;

            switch (itemType)
            {
                case ProductPage.ModelTypeAlias:
                    {
                        _productIndexFactory.TryPopulateIndex(context?.GetById(nodeId) as ProductPage, updatedValues);
                        break;
                    }

            }
        }

        public void Terminate()
        { }
    }
}