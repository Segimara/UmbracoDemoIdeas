using Examine;
using Examine.Lucene;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Hosting;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Index
{
    public class SearchableContentIndex : UmbracoExamineIndex, IUmbracoContentIndex
    {
        public SearchableContentIndex(
            ILoggerFactory loggerFactory,
            string name,
            IOptionsMonitor<LuceneDirectoryIndexOptions> indexOptions,
            IHostingEnvironment hostingEnvironment,
            IRuntimeState runtimeState) : base(loggerFactory, name, indexOptions, hostingEnvironment, runtimeState)
        {
            var namedOptions = indexOptions.Get(name);

            if (namedOptions == null)
            {
                throw new InvalidOperationException($"No named {typeof(LuceneDirectoryIndexOptions)} options with name {name}");
            }

            if (namedOptions.Validator is IContentValueSetValidator contentValueSetValidator)
            {
                PublishedValuesOnly = true;
            }
        }

        void IIndex.IndexItems(IEnumerable<ValueSet> values)
        {
            base.PerformIndexItems(
                values.Where(x =>
                    x.ItemType is
                        ProductPage.ModelTypeAlias),
                OnIndexOperationComplete);
        }
    }
}