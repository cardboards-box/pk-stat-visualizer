namespace PokeDexSharp.Database.Services.Games;

using Models;

public interface IGameDbService : IKeyedOrm<Game>
{

}

internal class GameDbService(IOrmService orm) : KeyedCacheOrm<Game>(orm), IGameDbService
{

}
