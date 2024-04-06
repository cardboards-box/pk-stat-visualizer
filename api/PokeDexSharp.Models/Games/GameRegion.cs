namespace PokeDexSharp.Models;

[Table("pk_game_regions")]
public class GameRegion : LocalizedDbObject
{
    public const string DEFAULT = "unknown";

    [Column("generation_id")]
    public required Guid GenerationId { get; set; }

    [Column("pokedexes")]
    public Guid[] Pokedexes { get; set; } = [];
}
