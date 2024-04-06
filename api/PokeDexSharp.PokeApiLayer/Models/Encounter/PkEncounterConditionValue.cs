namespace PokeDexSharp.PokeApiLayer;

[PkResource("encounter-condition-value/([0-9]{1,})")]
public class PkEncounterConditionValue : PkLocalizedBase
{
    [JsonPropertyName("condition")]
    public required PkResource Condition { get; set; }
}
