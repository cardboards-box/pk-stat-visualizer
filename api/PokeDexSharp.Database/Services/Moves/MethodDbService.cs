namespace PokeDexSharp.Database.Services.Moves;

using Models;

public interface IMethodDbService : IKeyedOrm<MoveMethod>
{

}

internal class MethodDbService(IOrmService orm) : KeyedCacheOrm<MoveMethod>(orm), IMethodDbService
{
}
