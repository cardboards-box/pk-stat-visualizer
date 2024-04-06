namespace PokeDexSharp.Models;

[Table("pk_move_targets")]
public class MoveTarget : LocalizedDbObject
{
    [Column("description")]
    public Localization[] Description { get; set; } = [];
}
