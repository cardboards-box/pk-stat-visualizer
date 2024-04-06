namespace PokeDexSharp.Database.Services.Games;

using Models;

public interface IGroupDbService : IKeyedOrm<GameGroup>
{

}

internal class GroupDbService(IOrmService orm): KeyedCacheOrm<GameGroup>(orm), IGroupDbService
{

}
