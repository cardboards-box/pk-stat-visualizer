namespace PokeDexSharp.PokeApiLayer.Migration.Migrations.Games;

using Models;

internal class GameMigration(
    ICommonService _common,
    ILogger<GameMigration> _logger): BaseMigration<Game, PkGame>(_common, _logger)
{
    public override IOrmMap<Game> Db => Common.Db.Games.Games;

    public override Task<Game?> Single(PkGame game)
    {
        return Task.FromResult<Game?>(new Game
        {
            Key = game.Name,
            Name = Common.From(game).ToArray()
        });
    }
}
