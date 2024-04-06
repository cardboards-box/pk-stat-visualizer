namespace PokeDexSharp.Models;

[Table("pk_elements")]
public class Element : LocalizedDbObject
{
    [Column("class_id")]
    public Guid ClassId { get; set; }

    [Column("damage_double_from")]
    public Guid[] DamageDoubleFrom { get; set; } = [];

    [Column("damage_double_to")]
    public Guid[] DamageDoubleTo { get; set; } = [];

    [Column("damage_half_from")]
    public Guid[] DamageHalfFrom { get; set; } = [];

    [Column("damage_half_to")]
    public Guid[] DamageHalfTo { get; set; } = [];

    [Column("damage_no_from")]
    public Guid[] DamageNoFrom { get; set; } = [];

    [Column("damage_no_to")]
    public Guid[] DamageNoTo { get; set; } = [];
}
