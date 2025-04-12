using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace UmbracoDemoIdeas.Core.Infrastructure.Extentions;
internal static class PublishedContentExtentions
{
    public static T? GetPageOfType<T>(this IPublishedContent content)
        where T : class, IPublishedContent
    {
        return content.FirstChild<T>();
    }

    public static IEnumerable<T> GetPagesOfType<T>(this IPublishedContent content)
        where T : class, IPublishedContent
    {
        return content.AncestorsOrSelf<T>();
    }
}
