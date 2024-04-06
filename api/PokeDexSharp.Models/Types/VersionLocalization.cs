namespace PokeDexSharp.Models;

[Type("pk_version_localization")]
public class VersionLocalization
{
    public required string Code { get; set; }

    public required string Value { get; set; }

    public required Guid Generation { get; set; }
}
