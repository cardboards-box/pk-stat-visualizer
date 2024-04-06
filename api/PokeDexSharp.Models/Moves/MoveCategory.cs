namespace PokeDexSharp.Models;

[Table("pk_move_categories")]
public class MoveCategory : LocalizedDbObject
{
    public const string DEFAULT = "unknown";
}
