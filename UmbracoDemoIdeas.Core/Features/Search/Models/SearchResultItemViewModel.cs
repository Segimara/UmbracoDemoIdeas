using UmbracoDemoIdeas.Core.Infrastructure.Models;

namespace UmbracoDemoIdeas.Core.Features.Search.Models;
public class SearchResultItemViewModel
{
    public required string Type { get; set; }
    public required string Name { get; set; }
    public required string Url { get; set; }
    public LinkDto? CategoryLink { get; set; }
    public string? ShortDescription { get; set; }
    public ImageDto? PreviewImage { get; set; }
    public IEnumerable<LinkDto>? Breadcrumbs { get; set; }
}
