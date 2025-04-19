namespace UmbracoDemoIdeas.Core.Infrastructure.Models;
public class PriceDto
{
    public decimal Value { get; set; }
    public string DisplayValue => $"{Value.ToString("0.00")} {CurrencyCode}";
    public string CurrencyCode { get; set; }
}
