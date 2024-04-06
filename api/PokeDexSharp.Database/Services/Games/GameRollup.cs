namespace PokeDexSharp.Database.Services.Games;

public interface IGameRollup
{
    IRegionDbService Regions { get; }

    IGenerationDbService Generations { get; }

    ILocationDbService Locations { get; }

    IGameDbService Games { get; }

    IGroupDbService Groups { get; }

    IMapDbService GroupMaps { get; }
}

internal class GameRollup(
    IRegionDbService regions,
    IGenerationDbService generations,
    ILocationDbService locations,
    IGameDbService games,
    IGroupDbService groups,
    IMapDbService map) : IGameRollup
{
    public IRegionDbService Regions => regions;

    public IGenerationDbService Generations => generations;

    public ILocationDbService Locations => locations;

    public IGameDbService Games => games;

    public IGroupDbService Groups => groups;

    public IMapDbService GroupMaps => map;
}
