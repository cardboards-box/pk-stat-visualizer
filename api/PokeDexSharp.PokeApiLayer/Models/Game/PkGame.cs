namespace PokeDexSharp.PokeApiLayer;

[PkResource("version/([0-9]{1,})")]
public class PkGame : PkLocalizedBase
{
    [JsonPropertyName("version_group")]
    public required PkResource Generation { get; set; }
}
