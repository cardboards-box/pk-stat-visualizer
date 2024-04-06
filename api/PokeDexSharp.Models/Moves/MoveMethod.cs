namespace PokeDexSharp.Models;

[Table("pk_move_methods")]
public class MoveMethod : LocalizedDbObject
{
    [Column("description")]
    public Localization[] Description { get; set; } = [];
}
