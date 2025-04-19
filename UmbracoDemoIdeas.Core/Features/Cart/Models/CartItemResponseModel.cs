using UmbracoDemoIdeas.Core.Infrastructure.Models;

namespace UmbracoDemoIdeas.Core.Features.Cart.Models;
public class CartItemResponseModel
{
    public Guid ProductId { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public PriceDto? UnitPrice { get; set; }
    public PriceDto? TotalPrice { get; set; }
    public ImageDto? Image { get; set; }
}
