namespace PokeDexSharp.PokeApiLayer;

[PkResource("pokemon-habitat/([0-9]{1,})")]
public class PkPokemonHabitat : PkLocalizedBase
{
    [JsonPropertyName("pokemon_species")]
    public PkResource[] Species { get; set; } = [];
}
