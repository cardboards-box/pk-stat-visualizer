namespace PokeDexSharp.Database.Services;

using Models;

public interface IElementDbService : IKeyedOrm<Element>
{

}

internal class ElementDbService(IOrmService orm): KeyedOrm<Element>(orm), IElementDbService
{

}
