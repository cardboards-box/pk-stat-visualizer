namespace PokeDexSharp.PokeApiLayer.Migration.Migrations.Moves;

using Models;

internal class MoveAilmentMigration(
    ICommonService _common,
    ILogger<MoveAilmentMigration> _logger) : BaseMigration<MoveAilment, PkMoveAilment>(_common, _logger)
{
    public override IOrmMap<MoveAilment> Db => Common.Db.Moves.Ailments;

    public override Task<MoveAilment?> Single(PkMoveAilment ailment)
    {
        return Task.FromResult<MoveAilment?>(new MoveAilment
        {
            Key = ailment.Name,
            Name = Common.From(ailment).ToArray()
        });
    }
}
