using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace UmbracoDemoIdeas.Core.Infrastructure.Extentions;
public static class CollectionExtensions
{
    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source)
    {
        return source ?? Enumerable.Empty<T>();
    }

    public static IEnumerable<T> SkipNullElements<T>(this IEnumerable<T>? source)
    {
        return source.EmptyIfNull().Where(x => x != null).ToList();
    }

    public static bool IsEmpty<T>(this IEnumerable<T>? source)
    {
        return !source.EmptyIfNull().Any();
    }

    public static IList<T> EmptyIfNull<T>(this IList<T>? source)
    {
        return source ?? new List<T>();
    }

    public static IList<T> SkipNullElements<T>(this IList<T>? source)
    {
        return source.EmptyIfNull().Where(x => x != null).ToList();
    }

    public static bool IsEmpty<T>(this IList<T>? source)
    {
        return !source.EmptyIfNull().Any();
    }
    public static bool IsNotEmpty<T>(this IList<T>? source)
    {
        return source.EmptyIfNull().Any();
    }
    public static bool IsNotEmpty<T>(this IEnumerable<T>? source)
    {
        return source.EmptyIfNull().Any();
    }

    public static T? PickRandom<T>(this IEnumerable<T> source)
    {
        return source.PickRandom(1).FirstOrDefault();
    }

    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
    {
        return source.Shuffle().Take(count);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(x => Guid.NewGuid());
    }

    public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
    {
        var sourceList = source as List<T> ?? source.ToList();
        for (var index = 0; index < sourceList.Count; index += chunkSize)
        {
            yield return sourceList.Skip(index).Take(chunkSize);
        }
    }
    public static string ToJson<T>(this T data, JsonNamingPolicy namingPolicy = null)
    {
        return data != null
            ? JsonSerializer.Serialize(data, GetJsonSerializerOptions(namingPolicy))
            : string.Empty;
    }

    private static JsonSerializerOptions GetJsonSerializerOptions(JsonNamingPolicy namingPolicy = null)
    {
        namingPolicy ??= JsonNamingPolicy.CamelCase;

        var encoderSettings = new TextEncoderSettings();
        encoderSettings.AllowRange(UnicodeRanges.BasicLatin);

        return new JsonSerializerOptions
        {
            PropertyNamingPolicy = namingPolicy,
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(encoderSettings),
        };
    }

    public static string Join(this IEnumerable<string>? source, string separator)
    {
        return string.Join(separator, source.EmptyIfNull());
    }

    public static bool IsFieldUnique(this IEnumerable<int> first, IEnumerable<int> second)
    {
        var firstSet = new HashSet<int>(first);
        var secondSet = new HashSet<int>(second);

        return !firstSet.Overlaps(secondSet);
    }
}
