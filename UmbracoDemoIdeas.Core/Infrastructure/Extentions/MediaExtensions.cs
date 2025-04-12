using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace UmbracoDemoIdeas.Core.Infrastructure.Extentions;
public static class MediaExtensions
{
    public static string Alt(this MediaWithCrops image)
    {
        var alt = image.Value<string>(MediaPropertyAlias.Alt);

        return string.IsNullOrEmpty(alt) ? image.Name : alt;
    }

    public static string Title(this MediaWithCrops image)
    {
        var title = image.Value<string>(MediaPropertyAlias.Title);

        return string.IsNullOrEmpty(title) ? image.Name : title;
    }

    public static int Width(this MediaWithCrops image)
    {
        return image.Value<int>(Umbraco.Cms.Core.Constants.Conventions.Media.Width);
    }

    public static int Height(this MediaWithCrops image)
    {
        return image.Value<int>(Umbraco.Cms.Core.Constants.Conventions.Media.Height);
    }

    public static string Extension(this MediaWithCrops image)
    {
        var extension = image.Value<string>(Umbraco.Cms.Core.Constants.Conventions.Media.Extension) ?? string.Empty;

        return extension.Equals("svg", StringComparison.OrdinalIgnoreCase) ? "svg+xml" : extension;
    }
    public static string ExtensionWithOutChanges(this MediaWithCrops image)
    {
        var extension = image.Value<string>(Umbraco.Cms.Core.Constants.Conventions.Media.Extension) ?? string.Empty;

        return extension;
    }

    public static bool IsImageType(this MediaWithCrops? media)
    {
        if (media == null) return false;
        var extension = media.Extension();

        return extension.Equals("jpg", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals("jpeg", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals("png", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals("gif", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals("webp", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals("svg+xml", StringComparison.OrdinalIgnoreCase);
    }
}

public class MediaPropertyAlias
{
    public const string Title = "title";
    public const string Alt = "alt";
}