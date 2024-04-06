namespace PokeDexSharp.Models;

public abstract class KeyedDbObject : DbObject
{
    [Column("key", Unique = true)]
    public required string Key { get; set; }
}
