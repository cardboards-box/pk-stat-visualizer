namespace PokeDexSharp.Models;

[Type("pk_extended_localization")]
public class ExtendedLocalization
{
    public required string Code { get; set; }

    public required string Value { get; set; }

    public required string Extended { get; set; }
}
