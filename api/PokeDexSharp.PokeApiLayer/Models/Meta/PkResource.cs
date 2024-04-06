namespace PokeDexSharp.PokeApiLayer;

public class PkResource
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("url")]
    public required string Url { get; set; }
}

public interface IPkResource
{
    public PkResource Resource { get; set; }
}