using Microsoft.AspNetCore.Mvc;
using UmbracoDemoIdeas.Core.Features.Category.Facade;
using UmbracoDemoIdeas.Core.Features.Common.Attribute;

namespace UmbracoDemoIdeas.Core.Features.Category;

[ApiController]
[UmbracoAPIController]
[ApiExplorerSettings(GroupName = "Categories")]
public class CategoriesController(CategoriesFacade categoriesFacade) : Controller
{
    [HttpGet]
    [UmbracoAPIAction]
    public async Task<IActionResult> Categories()
    {
        var vm = await categoriesFacade.GetCategoriesAsync();

        return Ok(vm);
    }
}