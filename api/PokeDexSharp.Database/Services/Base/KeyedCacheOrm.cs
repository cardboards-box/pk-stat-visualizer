namespace PokeDexSharp.Database.Services;

using Models;

public interface IKeyedOrm<T> : IOrmMap<T> where T: KeyedDbObject
{
    Task<T?> Fetch(string? key);
}

public abstract class KeyedCacheOrm<T>(IOrmService orm) : CacheOrm<T>(orm), IKeyedOrm<T> where T: KeyedDbObject
{
    public async Task<T?> Fetch(string? key)
    {
        if (string.IsNullOrEmpty(key)) return default;

        var all = await Get();
        return all.FirstOrDefault(x => x.Key.EqualsIc(key));
    }
}

public abstract class KeyedOrm<T>(IOrmService orm) : Orm<T>(orm), IKeyedOrm<T> where T: KeyedDbObject
{
    private static string? _fetchByKey;

    public async Task<T?> Fetch(string? key)
    {
        if (string.IsNullOrEmpty(key)) return default;

        _fetchByKey ??= Map.Select(t => t.With(a => a.Key));
        return (await Get(_fetchByKey, new { Key = key })).FirstOrDefault();
    }
}