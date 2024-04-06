namespace PokeDexSharp.Models;

[Table("pk_languages")]
public class Language : LocalizedDbObject
{
    [Column("language_code")]
    public required string LanguageCode { get; set; }

    [Column("country_code")]
    public required string CountryCode { get; set; }

    [Column("official")]
    public bool Official { get; set; }
}
