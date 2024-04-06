namespace PokeDexSharp.PokeApiLayer.Migration.Migrations.Games;

using Models;

internal class GameGroupMigration(
    ICommonService _common,
    ILogger<GameGroupMigration> _logger) : BaseMigration<GameGroup, PkVersionGroup>(_common, _logger)
{
    public override Type[] Requires => [typeof(GameGeneration), typeof(MoveMethod)];

    public override IOrmMap<GameGroup> Db => Common.Db.Games.Groups;

    public override async Task<GameGroup?> Single(PkVersionGroup group)
    {
        var key = string.IsNullOrEmpty(group.Generation.Name) ? GameGeneration.DEFAULT : group.Generation.Name;
        var generation = await Common.Db.Games.Generations.Fetch(key);
        if (generation is null)
        {
            _logger.LogWarning("Group {Group} has no generation - From Database", group.Name);
            return null;
        }

        var allMoves = await Common.Db.Moves.Methods.Get();
        var map = allMoves.Where(t => group.MoveMethods.Any(m => m.Name == t.Key))
            .Select(t => t.Id)
            .ToArray();

        return new GameGroup
        {
            Key = group.Name,
            GenerationId = generation.Id,
            MoveLearnMethods = map,
            Ordinal = group.Order,
        };
    }

}
