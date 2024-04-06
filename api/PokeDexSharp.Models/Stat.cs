namespace PokeDexSharp.Models;

[Table("pk_stats")]
public class Stat : LocalizedDbObject
{
    [Column("class_id")]
    public required Guid ClassId { get; set; }

    [Column("is_battle_only")]
    public required bool IsBattleOnly { get; set; }
}
