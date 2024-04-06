namespace PokeDexSharp.Database.Services.Games;

using Models;

public interface IGenerationDbService : IKeyedOrm<GameGeneration>
{
}

internal class GenerationDbService(IOrmService orm) : KeyedCacheOrm<GameGeneration>(orm), IGenerationDbService
{

}
