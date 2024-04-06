namespace PokeDexSharp.PokeApiLayer.Migration;

using Models;

public abstract class BaseMigration<TTo, TFrom>(
    ICommonService _common,
    ILogger _logger) : IMigration where TTo : DbObject
{
    private TypeCache? _type;
    private string? _name;

    public virtual Type[] Requires { get; } = [typeof(Language)];

    public virtual Type Target => typeof(TTo);

    public ICommonService Common => _common;

    public ILogger Logger => _logger;

    public abstract IOrmMap<TTo> Db { get; }

    public virtual TypeCache Type => _type ??= _common.Ref.Cache<TFrom>()
        ?? throw new ArgumentException("Cannot find type cache for: " + typeof(TFrom).Name);

    public virtual string Name => _name ??= Type.Attribute.Name.TrimStart("/api/v2/").Split('/').First();

    public virtual async Task Migrate()
    {
        _logger.LogInformation("Migrating {name}...", Name);
        await Before();

        var ids = _common.Ref.GetAllIds<TFrom>();

        if (ids.Length == 0)
        {
            _logger.LogWarning("No {name}s found.", Name);
            return;
        }

        var items = new List<(TTo to, TFrom from)>();

        foreach (var id in ids)
        {
            var from = await _common.Ref.GetResource<TFrom>(id);
            if (from is null)
            {
                _logger.LogWarning("{name} {id} not found.", Name, id);
                continue;
            }

            var first = await Single(from);
            if (first is not null)
            {
                first.Id = await Db.Upsert(first);
                items.Add((first, from));
                _logger.LogDebug("{name} {id} migrated.", Name, id);
                continue;
            }

            var worked = false;
            await foreach (var to in Multiple(from))
            {
                if (to is null)
                {
                    _logger.LogWarning("Failed to migrate {name} {id} >> Migration resulted in null.", Name, id);
                    continue;
                }

                worked = true;
                to.Id = await Db.Upsert(to);
                items.Add((to, from));
            }

            if (worked)
                _logger.LogDebug("{name} {id} migrated.", Name, id);
            else
                _logger.LogWarning("Failed to migrate {name} {id} >> No migration succeeded.", Name, id);
        }

        await After(items.ToArray());
        _logger.LogInformation("{name} migrated.", Name);
    }

    public virtual Task After((TTo to, TFrom from)[] items) => Task.CompletedTask;

    public virtual Task Before() => Task.CompletedTask;

    public virtual Task<TTo?> Single(TFrom from) => Task.FromResult<TTo?>(null);

    public virtual IAsyncEnumerable<TTo> Multiple(TFrom from) => AsyncEnumerable.Empty<TTo>();
}
