namespace PokeDexSharp.PokeApiLayer;

[PkResource("move-damage-class/([0-9]{1,})")]
public class PkMoveDamageClass : PkLocalizedDescBase
{
    [JsonPropertyName("moves")]
    public PkResource[] Moves { get; set; } = [];
}
