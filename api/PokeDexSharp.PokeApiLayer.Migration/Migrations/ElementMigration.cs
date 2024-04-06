namespace PokeDexSharp.PokeApiLayer.Migration.Migrations;

using Models;

internal class ElementMigration(
    ICommonService _common,
    ILogger<ElementMigration> _logger) : BaseMigration<Element, PkType>(_common, _logger)
{
    public override Type[] Requires => [typeof(MoveClass)];

    public override IOrmMap<Element> Db => Common.Db.Elements;

    public override async Task<Element?> Single(PkType type)
    {
        var classKey = type.MoveDamageClass?.Name;
        if (string.IsNullOrEmpty(classKey))
        {
            _logger.LogWarning("Type {Type} has no damage class - From Source, using {default}", type.Name, MoveClass.DEFAULT);
            classKey = MoveClass.DEFAULT;
        }

        var @class = await Common.Db.Moves.Classes.Fetch(classKey);
        if (@class is null)
        {
            _logger.LogWarning("Type {Type} has no damage class - From Database", type.Name);
            return null;
        }

        return new Element
        {
            Key = type.Name,
            Name = Common.From(type).ToArray(),
            ClassId = @class.Id,
        };
    }

    public override async Task After((Element to, PkType from)[] items)
    {
        _logger.LogInformation("Starting cross mapping for elements...");

        var all = items.Select(t => t.to).ToDictionary(t => t.Key);
        foreach(var (item, type) in items)
        {
            Map(type.DamageRelations.DoubleFrom, t => item.DamageDoubleFrom = t, all);
            Map(type.DamageRelations.DoubleTo, t => item.DamageDoubleTo = t, all);
            Map(type.DamageRelations.HalfFrom, t => item.DamageHalfFrom = t, all);
            Map(type.DamageRelations.HalfTo, t => item.DamageHalfTo = t, all);
            Map(type.DamageRelations.NoFrom, t => item.DamageNoFrom = t, all);
            Map(type.DamageRelations.NoTo, t => item.DamageNoTo = t, all);

            await Db.Update(item);
        }

        _logger.LogInformation("Cross mapping for elements finished.");
    }

    public static void Map(PkResource[] resources, Action<Guid[]> setter, Dictionary<string, Element> all)
    {
        var ids = resources
            .Where(t => !string.IsNullOrEmpty(t.Name))
            .Select(t => all[t.Name!].Id)
            .ToArray();
        setter(ids);
    }
}
