using Examine;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoDemoIdeas.Core.Features.Common.Controllers;
using UmbracoDemoIdeas.Core.Features.Search;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Constants;
using UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Index;
using UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Index.Factories;
using UmbracoDemoIdeas.Core.Infrastructure.Providers;

namespace UmbracoDemoIdeas.Core;
public static class DependencyInjection
{
    public static IUmbracoBuilder RegisterCore(this IUmbracoBuilder builder)
    {
        builder.Services.Configure<UmbracoRenderingDefaultsOptions>(c =>
         {
             c.DefaultControllerType = typeof(DefaultPageController);
         });

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddScoped<UmbracoContentProvider>();
        builder.Services.AddScoped<ExamineSearcherAccessor>();
        builder.Components().Append<SearchableContentIndexComponent>();
        builder.Services.AddScoped<ISearchService, SearchService>();
        builder.Services.AddSingleton<ProductIndexFactory>();
        builder.Services.AddScoped<SearchModelsFactory>();
        builder.Services.AddExamineLuceneIndex<SearchableContentIndex, ConfigurationEnabledDirectoryFactory>(IndexType.SearchableContentIndex);


        return builder;
    }
}

