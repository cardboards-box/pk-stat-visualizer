namespace PokeDexSharp.Database.Services.Moves;

using Models;

public interface ITargetDbService : IKeyedOrm<MoveTarget>
{

}

internal class TargetDbService(IOrmService orm) : KeyedCacheOrm<MoveTarget>(orm), ITargetDbService
{

}
