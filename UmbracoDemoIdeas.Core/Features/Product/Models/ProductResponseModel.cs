using UmbracoDemoIdeas.Core.Infrastructure.Models;

public class ProductResponseModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? FullDescription { get; set; }
    public PriceDto? Price { get; set; }
    public ImageDto? Image { get; set; }
    public Guid CategoryId { get; set; }
    public string? Category { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
}
