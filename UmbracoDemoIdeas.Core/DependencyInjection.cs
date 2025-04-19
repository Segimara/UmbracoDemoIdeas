using Examine;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Infrastructure.Examine;
using UmbracoDemoIdeas.Core.Features.Cart.Facade;
using UmbracoDemoIdeas.Core.Features.Cart.Factory;
using UmbracoDemoIdeas.Core.Features.Cart.Services;
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
using UmbracoDemoIdeas.Core.Infrastructure.Providers;

namespace UmbracoDemoIdeas.Core;

public static class DependencyInjection
{
    public static IUmbracoBuilder RegisterCore(this IUmbracoBuilder builder)
    {
        // Providers
        builder.Services.AddScoped<UmbracoContentProvider>();
        builder.Services.AddScoped<ExamineSearcherAccessor>();

        // Factories
        builder.Services.AddSingleton<ProductIndexFactory>();
        builder.Services.AddScoped<CommerceFactory>();
        builder.Services.AddScoped<SearchModelsFactory>();
        builder.Services.AddScoped<ProductModelsFactory>();
        builder.Services.AddScoped<CategoryModelsFactory>();
        builder.Services.AddScoped<CartModelsFactory>();

        // Facades
        builder.Services.AddScoped<ProductsFacade>();
        builder.Services.AddScoped<CategoriesFacade>();
        builder.Services.AddScoped<CartFacade>();

        // Services
        builder.Services.AddScoped<ISearchService, SearchService>();
        builder.Services.AddScoped<CartService>();

        // Indexes
        builder.Services.AddExamineLuceneIndex<SearchableContentIndex, ConfigurationEnabledDirectoryFactory>(IndexType.SearchableContentIndex);

        // Components
        builder.Components().Append<SearchableContentIndexComponent>();

        return builder;
    }
}