namespace PokeDexSharp.PokeApiLayer;

[PkResource("item-pocket/([0-9]{1,})")]
public class PkItemPocket : PkLocalizedBase
{
    [JsonPropertyName("categories")]
    public PkResource[] Categories { get; set; } = [];
}
