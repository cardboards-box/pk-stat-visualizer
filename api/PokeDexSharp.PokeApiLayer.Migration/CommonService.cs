namespace PokeDexSharp.PokeApiLayer.Migration;

using Models;

public interface ICommonService
{
    IPokeApiService Api { get; }

    IDbService Db { get; }

    IReferenceFileService Ref { get; }

    IEnumerable<Localization> From(PkLocalizedBase localized);

    IEnumerable<Localization> From(PkLocalizedDescription[] items);
}

internal class CommonService(
    ILogger<CommonService> _logger,
    IPokeApiService _api,
    IDbService _db,
    IReferenceFileService _ref) : ICommonService
{
    public IPokeApiService Api => _api;

    public IDbService Db => _db;

    public IReferenceFileService Ref => _ref;

    public IEnumerable<Localization> From(PkLocalizedBase localized)
    {
        foreach (var item in localized.Localizations)
        {
            if (string.IsNullOrEmpty(item.Resource?.Name))
            {
                _logger.LogWarning("Empty resource name: {value}[{id}] >> {name}", localized.Name, localized.Id, item.Name);
                continue;
            }

            yield return new Localization
            {
                Code = item.Resource.Name,
                Value = item.Name
            };
        }
    }

    public IEnumerable<Localization> From(PkLocalizedDescription[] items)
    {
        foreach (var item in items)
        {
            if (string.IsNullOrEmpty(item.Resource?.Name))
            {
                _logger.LogWarning("Empty resource name: {name}", item.Description);
                continue;
            }

            yield return new Localization
            {
                Code = item.Resource.Name,
                Value = item.Description
            };
        }
    }
}
