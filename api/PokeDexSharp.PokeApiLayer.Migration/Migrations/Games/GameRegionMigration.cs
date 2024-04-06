namespace PokeDexSharp.PokeApiLayer.Migration.Migrations.Games;

using Models;

internal class GameRegionMigration(
    ICommonService _common,
    ILogger<GameRegionMigration> _logger) : BaseMigration<GameRegion, PkRegion>(_common, _logger)
{
    public override Type[] Requires => [typeof(GameGeneration)];

    public override IOrmMap<GameRegion> Db => Common.Db.Games.Regions;

    public override async Task<GameRegion?> Single(PkRegion region)
    {
        var key = string.IsNullOrEmpty(region.MainGeneration?.Name) ? GameGeneration.DEFAULT : region.MainGeneration.Name;
        var generation = await Common.Db.Games.Generations.Fetch(key);
        if (generation is null)
        {
            _logger.LogWarning("Generation {Generation} not found for region {Region}", region.MainGeneration?.Name, region.Name);
            return null;
        }

        return new GameRegion
        {
            Key = region.Name,
            Name = Common.From(region).ToArray(),
            GenerationId = generation.Id
        };
    }

    public override async Task After((GameRegion to, PkRegion from)[] items)
    {
        var generation = await Common.Db.Games.Generations.Fetch(GameGeneration.DEFAULT);
        if (generation is null)
        {
            _logger.LogWarning("Could not create default region -> Default generation was not found");
            return;
        }

        var unknown = new GameRegion
        {
            Key = GameRegion.DEFAULT,
            GenerationId = generation.Id,
            Name =
            [
                new Localization { Code = "en", Value = GameRegion.DEFAULT }
            ]
        };

        await Db.Upsert(unknown);
    }

}
