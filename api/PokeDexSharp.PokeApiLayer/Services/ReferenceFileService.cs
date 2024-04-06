using System.Reflection;

namespace PokeDexSharp.PokeApiLayer;

public interface IReferenceFileService
{
    Task<T?> GetResource<T>(PkResource resource);

    Task<T?> GetResource<T>(string path);

    Task<T[]> GetResources<T>(PkResource resource);

    Task<T[]> GetResources<T>(string path);

    Task<T?> GetResource<T>(int id);

    int[] GetAllIds<T>();

    TypeCache? Cache(Type type);

    TypeCache? Cache<T>();
}

internal class ReferenceFileService(
    IJsonService _json,
    IConfiguration _config) : IReferenceFileService
{
    public static TypeCache[]? _attributeCache;

    public string BasePath => _config["PokeApi:BasePath"] ?? throw new NullReferenceException("PokeApi:BasePath is not found");

    public static string[] PathSplit(string thing)
    {
        return thing.Split(["/", "\\"], StringSplitOptions.RemoveEmptyEntries);
    }

    public string CombinePath(string resource, bool append = true)
    {
        if (string.IsNullOrWhiteSpace(resource)) throw new ArgumentNullException(nameof(resource));

        resource = resource.TrimStart(['/', '\\']).ToLower();
        if (!resource.StartsWith("api/v2/") &&
            !resource.StartsWith("api\\v2\\"))
            resource = $"api/v2/{resource}";

        var parts = PathSplit(BasePath).Concat(PathSplit(resource)).ToArray();
        if (!parts.Last().Equals("index.json") && append)
            parts = [.. parts, "index.json"];

       return Path.Combine(parts);
    }

    public Task<T?> GetResource<T>(PkResource resource)
    {
        return GetResource<T>(resource.Url);
    }

    public async Task<T?> GetResource<T>(string path)
    {
        var resource = CombinePath(path);
        if (!File.Exists(resource)) return default;
        
        using var stream = File.OpenRead(resource);
        return await _json.Deserialize<T>(stream);
    }

    public async Task<T?> GetResource<T>(int id)
    {
        var cache = Cache<T>();
        if (cache is null) return default;

        var path = cache.Attribute.Name.Replace("([0-9]{1,})", id.ToString());
        return await GetResource<T>(path);
    }

    public Task<T[]> GetResources<T>(PkResource resource)
    {
        return GetResources<T>(resource.Url);
    }

    public async Task<T[]> GetResources<T>(string path)
    {
        var resources = await GetResource<T[]>(path);
        return resources ?? [];
    }

    public static TypeCache[] TypeCaches()
    {
        if (_attributeCache is not null)
            return _attributeCache;

        return _attributeCache = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Select(t => (t, t.GetCustomAttribute<PkResourceAttribute>()))
            .Where(t => t.Item2 is not null)
            .Select(t => new TypeCache(t.t, t.Item2!))
            .ToArray();
    }

    public TypeCache? Cache(Type type)
    {
        var cache = TypeCaches();
        return cache.FirstOrDefault(c => c.Type == type);
    }

    public TypeCache? Cache<T>() => Cache(typeof(T));

    public int[] GetAllIds<T>()
    {
        var cache = Cache<T>();
        if (cache is null) return [];

        var path = cache.Attribute.Name.TrimStart("/api/v2/").Split('/').First();
        var dir = CombinePath(path, false);
        if (!Directory.Exists(dir)) return [];

        var dirs = Directory.GetDirectories(dir);
        return dirs
            .Select(d => (int.TryParse(PathSplit(d).Last(), out var res), res))
            .Where(t => t.Item1)
            .Select(t => t.res)
            .OrderBy(i => i)
            .ToArray();
    }
}

public record class TypeCache(Type Type, PkResourceAttribute Attribute);
