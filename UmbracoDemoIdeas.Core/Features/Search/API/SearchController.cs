using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UmbracoDemoIdeas.Core.Features.Common.Attribute;
using UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Queries.ProductFilterQuery;

namespace UmbracoDemoIdeas.Core.Features.Search.API;

[ApiController]
[UmbracoAPIController]
[ApiExplorerSettings(GroupName = "Search")]
public class SearchController : Controller
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    [UmbracoAPIAction]
    public IActionResult Search([FromQuery, BindRequired] ProductFilterSearchTerm searchTerm)
    {
        var vm = _searchService.Search(searchTerm);

        return Ok(vm);
    }
}
