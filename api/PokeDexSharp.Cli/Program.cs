using PokeDexSharp;
using PokeDexSharp.Cli.Verbs;

var services = new ServiceCollection()
    .AddConfig(c => c.AddFile("appsettings.json", false, true), out var config);

await services.AddServices(config, c => 
    c.AddCore()
     .AddDatabase()
     .AddPokeApi()
     .AddPokeMigration(),
    AppDomain.CurrentDomain.GetAssemblies());

return await services
    .Cli(args, c =>
    {
        c.Add<MigrateVerb>();
    });