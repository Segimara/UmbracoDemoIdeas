namespace UmbracoDemoIdeas.Core.Features.Search.Infrastructure;
public class PaginatedItems<T>
{
    public IEnumerable<T>? Items { get; set; }
    public int TotalItems { get; set; }
    public int ItemsPerPage { get; set; }
    public int CurrentPage { get; set; }
    public int MaxPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPageget => CurrentPage < MaxPages;
}