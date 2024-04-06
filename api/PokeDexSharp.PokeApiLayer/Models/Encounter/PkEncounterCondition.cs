namespace PokeDexSharp.PokeApiLayer;

[PkResource("encounter-condition/([0-9]{1,})")]
public class PkEncounterCondition : PkLocalizedBase
{
    [JsonPropertyName("values")]
    public PkResource[] Values { get; set; } = [];
}
