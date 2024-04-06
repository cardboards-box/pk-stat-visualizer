namespace PokeDexSharp.PokeApiLayer;

[PkResource("item-category/([0-9]{1,})")]
public class PkItemCategory : PkLocalizedBase
{
    [JsonPropertyName("items")]
    public PkResource[] Items { get; set; } = [];

    [JsonPropertyName("pocket")]
    public required PkResource Pocket { get; set; }
}
