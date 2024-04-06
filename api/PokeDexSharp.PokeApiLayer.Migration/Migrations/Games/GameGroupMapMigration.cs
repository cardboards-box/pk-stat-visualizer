namespace PokeDexSharp.PokeApiLayer.Migration.Migrations.Games;

using Models;
using System;

internal class GameGroupMapMigration(
    ICommonService _common,
    ILogger<GameGroupMapMigration> _logger) : BaseMigration<GameGroupMap, PkVersionGroup>(_common, _logger)
{
    public override Type[] Requires => [typeof(GameGroup), typeof(Game)];

    public override IOrmMap<GameGroupMap> Db => Common.Db.Games.GroupMaps;

    public override async IAsyncEnumerable<GameGroupMap> Multiple(PkVersionGroup item)
    {
        var group = await Common.Db.Games.Groups.Fetch(item.Name);
        if (group is null)
        {
            _logger.LogError("Group not found: {name}", item.Name);
            yield break;
        }

        foreach(var version in item.Versions)
        {
            var game = await Common.Db.Games.Games.Fetch(version.Name);
            if (game is null)
            {
                _logger.LogError("Game not found: {name}", version.Name);
                continue;
            }

            yield return new GameGroupMap
            {
                GroupId = group.Id,
                GameId = game.Id,
            };
        }
    }
}
