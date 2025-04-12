using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoDemoIdeas.Core.Infrastructure.Extentions;

namespace UmbracoDemoIdeas.Core.Infrastructure.Models;
public class ImageDto
{
    public required string Url { get; set; }
    public required string MobileUrl { get; set; }
    public required string Alt { get; set; }
    public required string Title { get; set; }
    public required string Extension { get; set; }
    public required int OriginalWidth { get; set; }
    public required int OriginalHeight { get; set; }
    public bool Exists { get => !Url.IsNullOrWhiteSpace(); }

    public static ImageDto? Map(MediaWithCrops? image, UrlMode urlMode = UrlMode.Default)
    {
        if (image is null)
        {
            return default;
        }

        return new ImageDto
        {
            Url = image.Content.MediaUrl(mode: urlMode),
            MobileUrl = image.Content.Url(mode: urlMode),
            Alt = image.Alt(),
            Title = image.Title(),
            Extension = image.Extension(),
            OriginalWidth = image.Width(),
            OriginalHeight = image.Height(),
        };
    }
}