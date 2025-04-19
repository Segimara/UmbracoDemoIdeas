using Umbraco.Commerce.Core.Models;
using UmbracoDemoIdeas.Core.Features.Cart.Models;
using UmbracoDemoIdeas.Core.Features.Common.Factory;
using UmbracoDemoIdeas.Core.Infrastructure.Extentions;
using UmbracoDemoIdeas.Core.Infrastructure.Providers;

namespace UmbracoDemoIdeas.Core.Features.Cart.Factory;
public class CartModelsFactory(CommerceFactory commerceFactory, UmbracoContentProvider contentProvider)
{
    public CartResponseModel CreateCartModel(OrderReadOnly order)
    {
        var items = order.OrderLines.Select(line =>
        {
            var productId = Guid.Parse(line.Properties["productId"]);
            var product = contentProvider.GetProductById(productId);
            return new CartItemResponseModel
            {
                ProductId = productId,
                Name = line.Name,
                Quantity = (int)line.Quantity,
                UnitPrice = commerceFactory.CreatePriceDto(line.UnitPrice),
                TotalPrice = commerceFactory.CreatePriceDto(line.TotalPrice),
                Image = product?.PreveiwImage.ToImageDto()
            };
        }).ToList();

        return new CartResponseModel
        {
            OrderId = order.Id,
            Items = items,
            TotalPrice = commerceFactory.CreatePriceDto(order.TotalPrice),
            TotalItems = items.Sum(x => x.Quantity)
        };
    }
}