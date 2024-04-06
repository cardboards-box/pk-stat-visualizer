namespace PokeDexSharp.Models;

[Table("pk_game_locations")]
public class GameLocation : LocalizedDbObject
{
    [Column("region_id")]
    public required Guid RegionId { get; set; }
}
