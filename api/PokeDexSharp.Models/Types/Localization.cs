namespace PokeDexSharp.Models;

[Type("pk_localization")]
public class Localization
{
    public required string Code { get; set; }

    public required string Value { get; set; }
}
