namespace PokeDexSharp.Database.Services.Moves;

using Models;

public interface IAilmentDbService : IKeyedOrm<MoveAilment>
{

}

internal class AilmentDbService(IOrmService orm) : KeyedCacheOrm<MoveAilment>(orm), IAilmentDbService
{

}
