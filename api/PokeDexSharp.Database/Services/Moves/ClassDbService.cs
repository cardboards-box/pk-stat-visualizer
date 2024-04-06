namespace PokeDexSharp.Database.Services.Moves;

using Models;

public interface IClassDbService : IKeyedOrm<MoveClass>
{
}

internal class ClassDbService(IOrmService orm) : KeyedCacheOrm<MoveClass>(orm), IClassDbService
{
}
