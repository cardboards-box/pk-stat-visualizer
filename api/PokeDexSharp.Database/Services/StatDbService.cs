namespace PokeDexSharp.Database.Services;

using Models;

public interface IStatDbService : IKeyedOrm<Stat>
{

}

internal class StatDbService(IOrmService orm) : KeyedCacheOrm<Stat>(orm), IStatDbService
{

}
