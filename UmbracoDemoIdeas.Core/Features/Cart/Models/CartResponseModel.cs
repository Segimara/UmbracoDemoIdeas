using UmbracoDemoIdeas.Core.Infrastructure.Models;

namespace UmbracoDemoIdeas.Core.Features.Cart.Models;
public class CartResponseModel
{
    public Guid OrderId { get; set; }
    public List<CartItemResponseModel> Items { get; set; } = new List<CartItemResponseModel>();
    public PriceDto? TotalPrice { get; set; }
    public int TotalItems { get; set; }
}