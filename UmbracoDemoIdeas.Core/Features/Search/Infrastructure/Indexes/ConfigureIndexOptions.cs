using Examine;
using Examine.Lucene;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Constants;

namespace UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Indexes
{
    public class ConfigureIndexOptions
    {
        public void Configure(string name, LuceneDirectoryIndexOptions options)
        {
            switch (name)
            {
                case IndexType.SearchableContentIndex:
                    options.FieldDefinitions.AddOrUpdate(
                        new FieldDefinition(SearchFieldConstants.Description, FieldDefinitionTypes.FullText));
                    break;
            }
        }

        public void Configure(LuceneDirectoryIndexOptions options)
            => Configure(string.Empty, options);
    }
}