using Examine;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Commerce.Cms.Web.Api.Management.Models.Factories;
using UmbracoDemoIdeas.Core.Features.Category.Facade;
using UmbracoDemoIdeas.Core.Features.Category.Factory;
using UmbracoDemoIdeas.Core.Features.Common.Factory;
using UmbracoDemoIdeas.Core.Features.Product.Facade;
using UmbracoDemoIdeas.Core.Features.Product.Factory;
using UmbracoDemoIdeas.Core.Features.Search;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Constants;
using UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Index;
using UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Index.Factories;
using UmbracoDemoIdeas.Core.Infrastructure.Helpers;
using UmbracoDemoIdeas.Core.Infrastructure.Providers;

namespace UmbracoDemoIdeas.Core;
public static class DependencyInjection
{
    public static IUmbracoBuilder RegisterCore(this IUmbracoBuilder builder)
    {
        builder.Services.AddScoped<UmbracoContentProvider>();
        builder.Services.AddScoped<ExamineSearcherAccessor>();
        builder.Components().Append<SearchableContentIndexComponent>();
        builder.Services.AddScoped<ISearchService, SearchService>();
        builder.Services.AddSingleton<ProductIndexFactory>();
        builder.Services.AddScoped<SearchModelsFactory>();
        builder.Services.AddExamineLuceneIndex<SearchableContentIndex, ConfigurationEnabledDirectoryFactory>(IndexType.SearchableContentIndex);
        builder.Services.AddScoped<ProductSnapshotHelper>();
        builder.Services.AddScoped<ProductModelsFactory>();
        builder.Services.AddScoped<ProductsFacade>();
        builder.Services.AddScoped<CommerceFactory>();
        builder.Services.AddScoped<CategoryModelsFactory>();
        builder.Services.AddScoped<CartModelFactory>();
        builder.Services.AddScoped<CategoriesFacade>();


        return builder;
    }
}

