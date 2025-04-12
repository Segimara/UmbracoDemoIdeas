using System.Text;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;
using UmbracoDemoIdeas.Core.Features.Search.Infrastructure.Constants;

namespace UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Index.Factories;
internal class ProductIndexFactory
{
    public bool TryPopulateIndex(ProductPage? product, IDictionary<string, List<object>> updatedValues)
    {
        if (product is null)
        {
            return false;
        }

        var description = GetDescription(product);
        if (!description.IsNullOrWhiteSpace())
        {
            updatedValues.Add(SearchFieldConstants.Description, new List<object> { description });
        }

        //if (product.Category is not null)
        //{
        //    updatedValues.Add(SearchFieldConstants.CategoryId, new List<object> { product.Category.Id });
        //}

        return true;
    }

    private string GetDescription(ProductPage product)
    {
        return "";
        var combinedDescriptionBuilder = new StringBuilder();

        //if (product.ShortDescription is not null)
        //{
        //    combinedDescriptionBuilder.Append(product.ShortDescription.ToHtmlString());
        //}

        //combinedDescriptionBuilder.Append(" ");

        //if (product.DetailedDescription is not null)
        //{
        //    combinedDescriptionBuilder.Append(product.DetailedDescription.ToHtmlString());
        //}

        return combinedDescriptionBuilder.ToString();
    }
}
