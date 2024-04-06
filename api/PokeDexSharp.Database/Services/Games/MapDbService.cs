namespace PokeDexSharp.Database.Services.Games;

using Models;

public interface IMapDbService : IOrmMap<GameGroupMap>
{

}

internal class MapDbService(IOrmService orm) : CacheOrm<GameGroupMap>(orm), IMapDbService
{

}
