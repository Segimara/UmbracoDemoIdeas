namespace UmbracoDemoIdeas.Core.Features.Cart.Models;
public class UpdateCartItemRequestModel
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}