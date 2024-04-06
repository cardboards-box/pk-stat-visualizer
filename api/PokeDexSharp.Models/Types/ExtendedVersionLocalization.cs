namespace PokeDexSharp.Models;

[Type("pk_extended_version_localization")]
public class ExtendedVersionLocalization
{
    public required string Code { get; set; }

    public required string Value { get; set; }

    public required string Extended { get; set; }

    public required Guid Generation { get; set; }
}