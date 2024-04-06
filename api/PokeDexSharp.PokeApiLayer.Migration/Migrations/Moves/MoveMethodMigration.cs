namespace PokeDexSharp.PokeApiLayer.Migration.Migrations.Moves;

using Models;

internal class MoveMethodMigration(
    ICommonService _common,
    ILogger<MoveMethodMigration> _logger) : BaseMigration<MoveMethod, PkMoveLearnMethod>(_common, _logger)
{
    public override IOrmMap<MoveMethod> Db => Common.Db.Moves.Methods;

    public override Task<MoveMethod?> Single(PkMoveLearnMethod method)
    {
        return Task.FromResult<MoveMethod?>(new MoveMethod
        {
            Key = method.Name,
            Name = Common.From(method).ToArray(),
            Description = Common.From(method.Descriptions).ToArray()
        });
    }
}
