namespace PokeDexSharp.PokeApiLayer.Migration.Migrations.Games;

using Models;

internal class GameGenerationMigration(
    ICommonService _common,
    ILogger<GameGenerationMigration> _logger) : BaseMigration<GameGeneration, PkGeneration>(_common, _logger)
{
    public override IOrmMap<GameGeneration> Db => Common.Db.Games.Generations;

    public override Task<GameGeneration?> Single(PkGeneration generation)
    {
        return Task.FromResult<GameGeneration?>(new GameGeneration
        {
            Key = generation.Name,
            Name = Common.From(generation).ToArray(),
        });
    }

    public override Task After((GameGeneration to, PkGeneration from)[] items)
    {
        var unknown = new GameGeneration
        {
            Key = GameGeneration.DEFAULT,
            Name = 
            [
                new Localization { Code = "en", Value = GameGeneration.DEFAULT }
            ]
        };

        return Db.Upsert(unknown);
    }
}
