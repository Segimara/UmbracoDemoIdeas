using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UmbracoDemoIdeas.Core.Features.Cart.Facade;
using UmbracoDemoIdeas.Core.Features.Cart.Models;
using UmbracoDemoIdeas.Core.Features.Cart.Services;
using UmbracoDemoIdeas.Core.Features.Common.Attribute;

namespace UmbracoDemoIdeas.Core.Features.Cart;
[ApiController]
[UmbracoAPIController]
[ApiExplorerSettings(GroupName = "Cart")]
public class CartController(CartFacade cartFacade, CartService cartService) : Controller
{
    [HttpGet]
    [UmbracoAPIAction]
    public async Task<IActionResult> GetCart([FromQuery, BindRequired] GetCartRequestModel model)
    {
        var order = cartService.GetCurrentOrder();
        var vm = cartFacade.GetCart(order, model);
        return Ok(vm);
    }

    [HttpPost]
    [UmbracoAPIAction]
    public async Task<IActionResult> AddToCart([FromBody, BindRequired] AddToCartRequestModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var order = cartService.GetOrCreateCurrentOrder();
        var vm = await cartFacade.AddToCartAsync(order, model);
        return Ok(vm);
    }

    [HttpPut]
    [UmbracoAPIAction]
    public async Task<IActionResult> UpdateCartItem([FromBody, BindRequired] UpdateCartItemRequestModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var order = cartService.GetCurrentOrder();
        if (order == null)
            return NotFound("Cart not found.");

        var vm = await cartFacade.UpdateCartItemQuantityAsync(order, model);
        return Ok(vm);
    }

    [HttpDelete]
    [UmbracoAPIAction]
    public async Task<IActionResult> RemoveFromCart([FromBody, BindRequired] RemoveFromCartRequestModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var order = cartService.GetCurrentOrder();
        if (order == null)
            return NotFound("Cart not found.");

        var vm = await cartFacade.RemoveFromCartAsync(order, model);
        return Ok(vm);
    }

    [HttpDelete]
    [UmbracoAPIAction]
    public async Task<IActionResult> ClearCart([FromBody, BindRequired] ClearCartRequestModel model)
    {
        var order = cartService.GetCurrentOrder();
        if (order == null)
            return NotFound("Cart not found.");

        var vm = await cartFacade.ClearCartAsync(order, model);
        return Ok(vm);
    }
}