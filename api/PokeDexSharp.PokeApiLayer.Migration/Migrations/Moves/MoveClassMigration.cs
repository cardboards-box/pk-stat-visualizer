namespace PokeDexSharp.PokeApiLayer.Migration.Migrations.Moves;

using Models;

internal class MoveClassMigration(
    ICommonService _common,
    ILogger<MoveClassMigration> _logger) : BaseMigration<MoveClass, PkMoveDamageClass>(_common, _logger)
{
    public override IOrmMap<MoveClass> Db => Common.Db.Moves.Classes;

    public override Task<MoveClass?> Single(PkMoveDamageClass item)
    {
        return Task.FromResult<MoveClass?>(new MoveClass
        {
            Key = item.Name,
            Name = Common.From(item).ToArray(),
            Description = Common.From(item.Descriptions).ToArray()
        });
    }

    public override async Task Before()
    {
        var unknown = new MoveClass
        {
            Key = MoveClass.DEFAULT,
            Name =
            [
                new Localization
                {
                    Code = "en",
                    Value = MoveClass.DEFAULT
                }
            ],
            Description =
            [
                new Localization
                {
                    Code = "en",
                    Value = "No or an unknown damage type"
                }
            ]
        };

        await Db.Upsert(unknown);
    }
}
