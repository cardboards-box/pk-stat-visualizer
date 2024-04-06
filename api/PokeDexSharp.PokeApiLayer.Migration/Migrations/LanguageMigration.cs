namespace PokeDexSharp.PokeApiLayer.Migration.Migrations;

using Models;

internal class LanguageMigration(
    ICommonService _common, 
    ILogger<LanguageMigration> _logger) : BaseMigration<Language, PkLanguage>(_common, _logger)
{
    public override Type[] Requires => [];

    public override IOrmMap<Language> Db => Common.Db.Languages;

    public override Task<Language?> Single(PkLanguage language)
    {
        return Task.FromResult<Language?>(new Language
        {
            Key = language.Name,
            LanguageCode = language.Code,
            CountryCode = language.CountryCode,
            Official = language.Official,
            Name = Common.From(language).ToArray()
        });
    }
}
