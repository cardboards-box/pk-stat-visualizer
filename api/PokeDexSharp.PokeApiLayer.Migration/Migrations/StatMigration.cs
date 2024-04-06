namespace PokeDexSharp.PokeApiLayer.Migration.Migrations;

using Models;

internal class StatMigration(
    ICommonService _common,
    ILogger<StatMigration> _logger) : BaseMigration<Stat, PkStat>(_common, _logger)
{
    public override Type[] Requires => [typeof(MoveClass)];

    public override IOrmMap<Stat> Db => Common.Db.Stats;

    public override async Task<Stat?> Single(PkStat item)
    {
        var key = item.DamageClass?.Name ?? MoveClass.DEFAULT;
        var damageClass = await Common.Db.Moves.Classes.Fetch(key);
        if (damageClass == null)
        {
            _logger.LogWarning("Stat {name} references unknown damage class {key}", item.Name, key);
            return null;
        }

        return new Stat
        {
            Key = item.Name,
            Name = Common.From(item).ToArray(),
            ClassId = damageClass.Id,
            IsBattleOnly = item.IsBattleOnly,
        };
    }
}
