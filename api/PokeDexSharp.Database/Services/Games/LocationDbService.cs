namespace PokeDexSharp.Database.Services.Games;

using Models;

public interface ILocationDbService : IKeyedOrm<GameLocation>
{

}

internal class LocationDbService(IOrmService orm) : KeyedOrm<GameLocation>(orm), ILocationDbService
{

}
