using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Queries.ProductFilterQuery;

namespace UmbracoDemoIdeas.Core.Features.Search.API;
internal class UmbracoAPIAttribute : RouteAttribute
{
    public UmbracoAPIAttribute() : base("umbraco/api/[controller]/[action]") { }
}


[UmbracoAPI]
public class SearchController : UmbracoApiController
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    public IActionResult Search([FromQuery, BindRequired] ProductFilterSearchTerm searchTerm)
    {
        var vm = _searchService.Search(searchTerm);

        return Ok(vm);
    }
}
