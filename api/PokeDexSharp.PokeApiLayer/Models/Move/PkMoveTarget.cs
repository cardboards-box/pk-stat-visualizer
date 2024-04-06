namespace PokeDexSharp.PokeApiLayer;

[PkResource("move-target/([0-9]{1,})")]
public class PkMoveTarget : PkLocalizedDescBase
{
    [JsonPropertyName("moves")]
    public PkResource[] Moves { get; set; } = [];
}
