using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using UmbracoDemoIdeas.Core.Infrastructure.Models;

namespace UmbracoDemoIdeas.Core.Infrastructure.Extentions;
public static class DtoModelsExtention
{
    public static ImageDto? ToImageDto(this MediaWithCrops? media, UrlMode urlMode = UrlMode.Default)
    {
        return ImageDto.Map(media, urlMode);
    }
    public static LinkDto? ToLinkDto(this Link? link)
    {
        return LinkDto.Map(link);
    }

}
