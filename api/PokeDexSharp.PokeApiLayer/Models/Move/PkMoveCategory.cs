namespace PokeDexSharp.PokeApiLayer;

[PkResource("move-category/([0-9]{1,})")]
public class PkMoveCategory : PkBase
{
    [JsonPropertyName("descriptions")]
    public PkLocalizedDescription[] Descriptions { get; set; } = [];

    [JsonPropertyName("moves")]
    public PkResource[] Moves { get; set; } = [];
}
