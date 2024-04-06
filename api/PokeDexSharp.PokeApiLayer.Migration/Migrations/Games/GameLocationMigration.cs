namespace PokeDexSharp.PokeApiLayer.Migration.Migrations.Games;

using Models;

internal class GameLocationMigration(
    ICommonService _common,
    ILogger<GameLocationMigration> _logger): BaseMigration<GameLocation, PkLocation>(_common, _logger)
{
    public override Type[] Requires => [typeof(GameRegion)];

    public override IOrmMap<GameLocation> Db => Common.Db.Games.Locations;

    public override async Task<GameLocation?> Single(PkLocation location)
    {
        var key = string.IsNullOrWhiteSpace(location.Region?.Name) ? GameRegion.DEFAULT : location.Region?.Name;
        var region = await Common.Db.Games.Regions.Fetch(key);
        if (region is null)
        {
            _logger.LogWarning("Region {key} not found for location {name}", key, location.Name);
            return null;
        }

        return new GameLocation
        {
            Key = location.Name,
            Name = Common.From(location).ToArray(),
            RegionId = region.Id
        };
    }
}
