using System.Globalization;
using Umbraco.Commerce.Core.Models;
using Umbraco.Commerce.Core.Services;
using Umbraco.Commerce.Extensions;
using UmbracoDemoIdeas.Core.Infrastructure.Models;

namespace UmbracoDemoIdeas.Core.Features.Common.Factory;
public class CommerceFactory(ICurrencyService currencyService)
{
    public PriceDto CreatePriceDto(IProductSnapshot snapshot)
    {
        var price = snapshot.TryCalculatePrice().Result;

        return CreatePriceDto(price);
    }
    public PriceDto CreatePriceDto(Price? price)
    {
        if (price is null)
        {
            return new();
        }

        var culture = CultureInfo.GetCultureInfo(currencyService.GetCurrency(price.CurrencyId).Code);
        var symbol = culture.NumberFormat.CurrencySymbol;

        return new PriceDto
        {
            Value = price,
            CurrencyCode = symbol
        };
    }
}
