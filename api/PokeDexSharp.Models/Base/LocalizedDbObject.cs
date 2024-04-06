namespace PokeDexSharp.Models;

public abstract class LocalizedDbObject : KeyedDbObject
{
    [Column("name")]
    public required Localization[] Name { get; set; } = [];
}
