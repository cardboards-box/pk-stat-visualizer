namespace PokeDexSharp.Database.Services.Moves;

using Models;

public interface ICategoryDbService : IKeyedOrm<MoveCategory>
{

}

internal class CategoryDbService(IOrmService orm) : KeyedCacheOrm<MoveCategory>(orm), ICategoryDbService
{

}
