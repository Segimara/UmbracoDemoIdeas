namespace UmbracoDemoIdeas.Core.Features.Search.SearchableContentIndex.Queries.ProductFilterQuery;
public class ProductFilterSearchTerm : BaseSearchTerm
{
    public Guid CategoryId { get; set; }

}

public class BaseSearchTerm
{
    public string? SearchTerm { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}