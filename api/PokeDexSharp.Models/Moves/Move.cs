namespace PokeDexSharp.Models;

[Table("pk_moves")]
public class Move : LocalizedDbObject
{
    [Column("class_id")]
    public required Guid ClassId { get; set; }

    [Column("generation_id")]
    public required Guid GenerationId { get; set; }

    [Column("element_id")]
    public required Guid ElementId { get; set; }

    [Column("target_id")]
    public required Guid TargetId { get; set; }

    [Column("category_id")]
    public required Guid CategoryId { get; set; }

    [Column("ailment_id")]
    public required Guid AilmentId { get; set; }

    [Column("stat_changes")]
    public required StatChange[] StatChanges { get; set; }

    [Column("effect")]
    public required ExtendedLocalization[] Effect { get; set; }

    [Column("flavor_text")]
    public required VersionLocalization[] FlavorText { get; set; }

    [Column("effect_changes")]
    public required ExtendedVersionLocalization[] EffectChanges { get; set; }

    [Column("effect_chance")]
    public required int? EffectChance { get; set; }

    [Column("power")]
    public required int? Power { get; set; }

    [Column("pp")]
    public required int? Pp { get; set; }

    [Column("priority")]
    public required int? Priority { get; set; }

    [Column("accuracy")]
    public required int? Accuracy { get; set; }

    [Column("crit_rate")]
    public required int? CritRate { get; set; }

    [Column("drain")]
    public required int? Drain { get; set; }

    [Column("flinch_chance")]
    public required int? FlinchChance { get; set; }

    [Column("healing")]
    public required int? Healing { get; set; }

    [Column("stat_chance")]
    public required int? StatChance { get; set; }

    [Column("max_hits")]
    public required int? MaxHits { get; set; }

    [Column("max_turns")]
    public required int? MaxTurns { get; set; }

    [Column("min_hits")]
    public required int? MinHits { get; set; }

    [Column("min_turns")]
    public required int? MinTurns { get; set; }

    [Column("ailment_chance")]
    public required int? AilmentChance { get; set; }
}

[Type("pk_stat_change")]
public class StatChange
{
    public required int Change { get; set; }

    public required Guid Id { get; set; }
}
