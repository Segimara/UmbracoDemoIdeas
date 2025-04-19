using Umbraco.Commerce.Common;
using Umbraco.Commerce.Core.Api;
using Umbraco.Commerce.Core.Models;
using Umbraco.Commerce.Core.Services;
using Umbraco.Commerce.Extensions;
using UmbracoDemoIdeas.Core.Features.Cart.Models;
using UmbracoDemoIdeas.Core.Infrastructure.Providers;

namespace UmbracoDemoIdeas.Core.Features.Cart.Services;

public class CartService(UmbracoContentProvider contentProvider,
    IUmbracoCommerceApi commerceApi,
    IUnitOfWorkProvider unitOfWorkProvider,
    IOrderService orderService)
{
    public OrderReadOnly? GetCurrentOrder()
    {
        var store = contentProvider.GetDefaultStore();
        if (store == null)
            return null;

        return commerceApi.GetCurrentOrder(store.Id);
    }

    public OrderReadOnly GetOrCreateCurrentOrder()
    {
        var store = contentProvider.GetDefaultStore();
        if (store == null)
            throw new InvalidOperationException("No store configured.");

        var order = commerceApi.GetOrCreateCurrentOrder(store.Id);

        return order;
    }

    public OrderReadOnly AddToCart(OrderReadOnly order, AddToCartRequestModel model)
    {
        using var uow = unitOfWorkProvider.Create();
        var writableOrder = order.AsWritable(uow);

        var product = contentProvider.GetProductById(model.ProductId);
        if (product == null)
            throw new ArgumentException("Product not found.", nameof(model.ProductId));

        writableOrder.AddProduct(product.Key.ToString(), model.Quantity);

        orderService.SaveOrder(writableOrder);
        uow.Complete();

        return writableOrder.AsReadOnly();
    }

    public OrderReadOnly UpdateCartItemQuantity(OrderReadOnly order, UpdateCartItemRequestModel model)
    {
        using var uow = unitOfWorkProvider.Create();
        var writableOrder = order.AsWritable(uow);

        var lineItem = writableOrder.OrderLines.FirstOrDefault(x => x.Properties["productId"] == model.ProductId.ToString());
        if (lineItem == null)
            throw new ArgumentException("Product not found in cart.", nameof(model.ProductId));

        if (model.Quantity <= 0)
        {
            writableOrder.RemoveOrderLine(lineItem.Id);
        }
        else
        {
            writableOrder.WithOrderLine(lineItem.Id).SetQuantity(model.Quantity);
        }

        orderService.SaveOrder(writableOrder);
        uow.Complete();

        return writableOrder.AsReadOnly();
    }

    public OrderReadOnly RemoveFromCart(OrderReadOnly order, RemoveFromCartRequestModel model)
    {
        using var uow = unitOfWorkProvider.Create();
        var writableOrder = order.AsWritable(uow);

        var lineItem = writableOrder.OrderLines.FirstOrDefault(x => x.Properties["productId"] == model.ProductId.ToString());
        if (lineItem == null)
            throw new ArgumentException("Product not found in cart.", nameof(model.ProductId));

        writableOrder.RemoveOrderLine(lineItem.Id);

        orderService.SaveOrder(writableOrder);
        uow.Complete();

        return writableOrder.AsReadOnly();
    }

    public OrderReadOnly ClearCart(OrderReadOnly order, ClearCartRequestModel model)
    {
        using var uow = unitOfWorkProvider.Create();
        var writableOrder = order.AsWritable(uow);

        writableOrder.ClearOrderLines();

        orderService.SaveOrder(writableOrder);
        uow.Complete();

        return writableOrder.AsReadOnly();
    }
}