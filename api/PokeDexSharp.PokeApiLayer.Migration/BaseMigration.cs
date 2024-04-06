namespace PokeDexSharp.PokeApiLayer.Migration;

using Models;

/// <summary>
/// Common implementation for all migrations
/// </summary>
/// <typeparam name="TTo">The database object to create</typeparam>
/// <typeparam name="TFrom">The poke-api resource to load from</typeparam>
/// <param name="_common">The <see cref="ICommonService"/> implementation</param>
/// <param name="_logger">The <see cref="ILogger"/> implementation</param>
public abstract class BaseMigration<TTo, TFrom>(
    ICommonService _common,
    ILogger _logger) : IMigration where TTo : DbObject
{
    private TypeCache? _type;
    private string? _name;

    /// <summary>
    /// What needs to be loaded before this migration can occur
    /// </summary>
    /// <remarks>
    /// By default everything requires <see cref="Language"/>s be loaded first.
    /// Well technically that isn't true, but it's good if languages are loaded first...
    /// </remarks>
    public virtual Type[] Requires { get; } = [typeof(Language)];

    /// <summary>
    /// The type of the database resource
    /// </summary>
    public virtual Type Target => typeof(TTo);

    /// <summary>
    /// The common service that contains the database service, common methods and type caches.
    /// </summary>
    public ICommonService Common => _common;

    /// <summary>
    /// The database service to upsert to
    /// </summary>
    public abstract IOrmMap<TTo> Db { get; }

    /// <summary>
    /// The type cache for the <typeparamref name="TFrom"/> type
    /// </summary>
    public virtual TypeCache Type => _type ??= _common.Ref.Cache<TFrom>()
        ?? throw new ArgumentException("Cannot find type cache for: " + typeof(TFrom).Name);

    /// <summary>
    /// The name of the poke-api resource that is being loaded (mostly for logging purposes)
    /// </summary>
    public virtual string Name => _name ??= Type.Attribute.Name.TrimStart("/api/v2/").Split('/').First();

    /// <summary>
    /// Start the migration process
    /// </summary>
    /// <returns></returns>
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

        await After([.. items]);
        _logger.LogInformation("{name} migrated.", Name);
    }

    /// <summary>
    /// Need to do anything after the migration has finished?
    /// </summary>
    /// <param name="items">All of the items that were found</param>
    /// <returns></returns>
    public virtual Task After((TTo to, TFrom from)[] items) => Task.CompletedTask;

    /// <summary>
    /// Need to do something before the migration starts?
    /// </summary>
    /// <returns></returns>
    public virtual Task Before() => Task.CompletedTask;

    /// <summary>
    /// Pass in the resource to migrate and get the database equivalent
    /// </summary>
    /// <param name="from">The resource to migrate</param>
    /// <returns>The database equivalent</returns>
    public virtual Task<TTo?> Single(TFrom from) => Task.FromResult<TTo?>(null);

    /// <summary>
    /// Pass in the resource to migrate and get the database equivalents
    /// </summary>
    /// <param name="from">The resource to migrate</param>
    /// <returns>The database equivalents</returns>
    /// <remarks>
    /// This should be the only method required,
    /// but I wrote <see cref="Single(TFrom)"/> first and
    /// I can't be bothered to update everything else.
    /// </remarks>
    public virtual IAsyncEnumerable<TTo> Multiple(TFrom from) => AsyncEnumerable.Empty<TTo>();
}
