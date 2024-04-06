namespace PokeDexSharp.PokeApiLayer.Migration.Migrations.Moves;

using Models;

internal class MoveTargetMigration(
    ICommonService _common,
    ILogger<MoveTargetMigration> _logger) : BaseMigration<MoveTarget, PkMoveTarget>(_common, _logger)
{
    public override IOrmMap<MoveTarget> Db => Common.Db.Moves.Targets;

    public override Task<MoveTarget?> Single(PkMoveTarget target)
    {
        return Task.FromResult<MoveTarget?>(new MoveTarget
        {
            Key = target.Name,
            Name = Common.From(target).ToArray(),
            Description = Common.From(target.Descriptions).ToArray()
        });
    }
}
