namespace PokeDexSharp;

using Database.Services;
using Database.Services.Moves;
using Database.Services.Games;

public interface IDbService
{
    ILanguageDbService Languages { get; }

    IMoveRollup Moves { get; }

    IGameRollup Games { get; }

    IElementDbService Elements { get; }
}

internal class DbService(
    ILanguageDbService language, 
    IMoveRollup move,
    IElementDbService elements,
    IGameRollup game) : IDbService
{
    public ILanguageDbService Languages => language;

    public IMoveRollup Moves => move;

    public IElementDbService Elements => elements;

    public IGameRollup Games => game;
}
