namespace PokeDexSharp.Models;

[Table("pk_move_classes")]
public class MoveClass : LocalizedDbObject
{
    public const string DEFAULT = "unknown";

    [Column("description")]
    public Localization[] Description { get; set; } = [];
}
