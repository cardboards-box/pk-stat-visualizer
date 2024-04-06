namespace PokeDexSharp.Models;

[Table("pk_game_group_maps")]
public class GameGroupMap : DbObject
{
    [Column("game_id", Unique = true)]
    public required Guid GameId { get; set; }

    [Column("group_id", Unique = true)]
    public required Guid GroupId { get; set; }
}
