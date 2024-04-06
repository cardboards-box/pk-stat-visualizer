namespace PokeDexSharp.Cli.Verbs;

using PokeApiLayer.Migration;

[Verb("migrate", HelpText = "Take data from the poke-api and load it into the database")]
public class MigrateVerbOptions { }

internal class MigrateVerb(
    ILogger<MigrateVerb> logger,
    IMigrationService _migration) : BooleanVerb<MigrateVerbOptions>(logger)
{
    public override async Task<bool> Execute(MigrateVerbOptions options, CancellationToken token)
    {
        await _migration.Migrate();
        return true;
    }
}
