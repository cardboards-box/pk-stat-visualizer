namespace PokeDexSharp.Models;

[Table("pk_game_groups")]
public class GameGroup : KeyedDbObject
{
    [Column("generation_id")]
    public required Guid GenerationId { get; set; }

    [Column("move_learn_methods")]
    public required Guid[] MoveLearnMethods { get; set; }

    [Column("ordinal")]
    public required int Ordinal { get; set; }
}
