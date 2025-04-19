using Microsoft.AspNetCore.Mvc;
using UmbracoDemoIdeas.Core.Features.Common.Attribute;
using UmbracoDemoIdeas.Core.Features.Product.Facade;
using UmbracoDemoIdeas.Core.Features.Product.Models;

namespace UmbracoDemoIdeas.Core.Features.Product;


[ApiController]
[UmbracoAPIController]
[ApiExplorerSettings(GroupName = "Products")]
public class ProductsController(ProductsFacade productsFacade) : Controller
{
    [HttpGet]
    [UmbracoAPIAction]
    public async Task<IActionResult> Products([FromQuery] ProductRequestModel model)
    {
        var vm = await productsFacade.GetProductsAsync(model.CategoryId);

        return Ok(vm);
    }
}