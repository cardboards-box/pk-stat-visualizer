namespace PokeDexSharp.Database.Services.Games;

using Models;

public interface IRegionDbService : IKeyedOrm<GameRegion>
{

}

internal class RegionDbService(IOrmService orm) : KeyedCacheOrm<GameRegion>(orm), IRegionDbService
{

}
