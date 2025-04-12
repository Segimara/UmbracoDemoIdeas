using Umbraco.Cms.Core.Models;

namespace UmbracoDemoIdeas.Core.Infrastructure.Models;
public class LinkDto
{
    public required string? Name { get; set; }
    public required string? Url { get; set; }
    public string? Target { get; set; }

    public static LinkDto? Map(Link? link)
    {
        if (link is null)
        {
            return default;
        }

        return new LinkDto
        {
            Name = link?.Name ?? null!,
            Url = link?.Url ?? null!,
            Target = link?.Target
        };
    }
}