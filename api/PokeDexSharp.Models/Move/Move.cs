namespace PokeDexSharp.Models;

[Table("pk_moves")]
public class Move : LocalizedDbObject
{
    [Column("class_id")]
    public Guid ClassId { get; set; }

    [Column("generation_id")]
    public Guid GenerationId { get; set; }

    [Column("element_id")]
    public Guid ElementId { get; set; }

    [Column("target_id")]
    public Guid TargetId { get; set; }

    [Column("category_id")]
    public Guid? CategoryId { get; set; }

    [Column("ailment_id")]
    public Guid? AilmentId { get; set; }

    [Column("effect")]
    public ExtendedLocalization[] Effect { get; set; } = [];

    [Column("flavor_text")]
    public VersionLocalization[] FlavorText { get; set; } = [];

    [Column("effect_changes")]
    public ExtendedVersionLocalization[] EffectChanges { get; set; } = [];

    [Column("effect_chance")]
    public int? EffectChance { get; set; }

    [Column("power")]
    public int? Power { get; set; }

    [Column("pp")]
    public int? Pp { get; set; }

    [Column("accuracy")]
    public int? Accuracy { get; set; }

    [Column("crit_rate")]
    public int? CritRate { get; set; }

    [Column("drain")]
    public int? Drain { get; set; }

    [Column("flinch_chance")]
    public int? FlinchChance { get; set; }

    [Column("healing")]
    public int? Healing { get; set; }

    [Column("stat_chance")]
    public int? StatChance { get; set; }

    [Column("max_hits")]
    public int? MaxHits { get; set; }

    [Column("max_turns")]
    public int? MaxTurns { get; set; }

    [Column("min_hits")]
    public int? MinHits { get; set; }

    [Column("min_turns")]
    public int? MinTurns { get; set; }

    [Column("priority")]
    public int? Priority { get; set; }

    [Column("ailment_chance")]
    public int? AilmentChance { get; set; }
}