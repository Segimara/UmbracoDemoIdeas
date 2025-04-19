namespace UmbracoDemoIdeas.Core.Features.Cart.Models;
public class AddToCartRequestModel
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; } = 1;
}
