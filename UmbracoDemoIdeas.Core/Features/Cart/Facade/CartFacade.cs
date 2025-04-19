using Umbraco.Commerce.Core.Models;
using UmbracoDemoIdeas.Core.Features.Cart.Factory;
using UmbracoDemoIdeas.Core.Features.Cart.Models;
using UmbracoDemoIdeas.Core.Features.Cart.Services;

namespace UmbracoDemoIdeas.Core.Features.Cart.Facade;
public class CartFacade(CartService cartService, CartModelsFactory cartModelsFactory)
{
    public CartResponseModel? GetCart(OrderReadOnly? order, GetCartRequestModel model)
    {
        if (order == null)
            return null;
        return cartModelsFactory.CreateCartModel(order);
    }

    public async Task<CartResponseModel> AddToCartAsync(OrderReadOnly order, AddToCartRequestModel model)
    {
        var updatedOrder = cartService.AddToCart(order, model);
        return cartModelsFactory.CreateCartModel(updatedOrder);
    }

    public async Task<CartResponseModel> UpdateCartItemQuantityAsync(OrderReadOnly order, UpdateCartItemRequestModel model)
    {
        var updatedOrder = cartService.UpdateCartItemQuantity(order, model);
        return cartModelsFactory.CreateCartModel(updatedOrder);
    }

    public async Task<CartResponseModel> RemoveFromCartAsync(OrderReadOnly order, RemoveFromCartRequestModel model)
    {
        var updatedOrder = cartService.RemoveFromCart(order, model);
        return cartModelsFactory.CreateCartModel(updatedOrder);
    }

    public async Task<CartResponseModel> ClearCartAsync(OrderReadOnly order, ClearCartRequestModel model)
    {
        var updatedOrder = cartService.ClearCart(order, model);
        return cartModelsFactory.CreateCartModel(updatedOrder);
    }
}