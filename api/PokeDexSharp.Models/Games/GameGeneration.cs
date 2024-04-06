namespace PokeDexSharp.Models;

[Table("pk_game_generations")]
public class GameGeneration : LocalizedDbObject
{
    public const string DEFAULT = "unknown";
}
