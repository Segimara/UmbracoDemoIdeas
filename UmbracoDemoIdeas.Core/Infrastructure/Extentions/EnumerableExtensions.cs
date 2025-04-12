using UmbracoDemoIdeas.Core.Features.Search.Infrastructure;

namespace UmbracoDemoIdeas.Core.Infrastructure.Extentions;
public static class EnumerableExtensions
{
    public static PaginatedItems<T> GetPaginatedItems<T>(this IEnumerable<T> values, int page = 1, int itemsPerPage = 10)
    {
        var result = new PaginatedItems<T>
        {
            TotalItems = values.EmptyIfNull().Count(),
            ItemsPerPage = itemsPerPage,
            CurrentPage = page,
            Items = values.EmptyIfNull().Paginate(page - 1, itemsPerPage)
        };

        return result;
    }

    public static PaginatedItems<T> GetPaginatedItems<T, TSource>(this IEnumerable<TSource>? values, Func<TSource, T> map, int page = 1, int itemsPerPage = 10)
    {
        var result = new PaginatedItems<T>
        {
            TotalItems = values.EmptyIfNull().Count(),
            ItemsPerPage = itemsPerPage,
            CurrentPage = page,
            Items = values.EmptyIfNull().Paginate(page - 1, itemsPerPage).Select(map)
        };

        return result;
    }

    private static IEnumerable<T> Paginate<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (pageIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageIndex), "pageIndex must be non-negative");
        }

        if (pageSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize), "pageSize must be positive");
        }

        return source.Skip(pageIndex * pageSize).Take(pageSize);
    }
}
