using UmbracoDemoIdeas.Core.Features.Search.Infrastructure;
using UmbracoDemoIdeas.Core.Infrastructure.Models;

namespace UmbracoDemoIdeas.Core.Features.Search.Models
{
    public class PaginatedSearchResults<T> : PaginatedItems<T>
    {
        public PaginatedSearchResults(PaginatedItems<T> items)
        {
            TotalItems = items.TotalItems;
            CurrentPage = items.CurrentPage;
            ItemsPerPage = items.ItemsPerPage;
            Items = items.Items;
        }

        public string? ColumnName { get; set; }
        public IEnumerable<LinkDto>? Links { get; set; }
    }
}
