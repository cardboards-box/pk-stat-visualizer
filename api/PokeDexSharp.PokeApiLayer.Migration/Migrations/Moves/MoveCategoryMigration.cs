namespace PokeDexSharp.PokeApiLayer.Migration.Migrations.Moves;

using Models;

internal class MoveCategoryMigration(
    ICommonService _common,
    ILogger<MoveCategoryMigration> _logger) : BaseMigration<MoveCategory, PkMoveCategory>(_common, _logger)
{
    public override IOrmMap<MoveCategory> Db => Common.Db.Moves.Categories;

    public override Task<MoveCategory?> Single(PkMoveCategory category)
    {
        return Task.FromResult<MoveCategory?>(new MoveCategory
        {
            Key = category.Name,
            Name = Common.From(category.Descriptions).ToArray()
        });
    }
}
