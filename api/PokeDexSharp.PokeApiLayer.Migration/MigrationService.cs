namespace PokeDexSharp.PokeApiLayer.Migration;

public interface IMigrationService
{
    Task Migrate();
}

internal class MigrationService(
    IServiceProvider _services,
    ILogger<MigrationService> _logger) : IMigrationService
{
    public IMigration[] GetMigrations()
    {
        const int MAX_ITERATIONS = 1000;
        //Get all of the migrations, ordered by the number of dependencies
        var all = _services
            .GetServices<IMigration>()
            .OrderBy(t => t.Requires.Length)
            .ToList(); 
        var types = all.ToDictionary(t => t.Target); //Create a map between the database type and the migration
        var indexed = new Dictionary<IMigration, int>(); //Keep track of the order of each of the migrations

        //Local method to make the logic easier to read and not nested in a while loop
        void Pass() 
        {
            //Iterate through the migrations and resolve the dependencies
            foreach (var migration in all!.ToArray())
            {
                if (indexed!.ContainsKey(migration)) //Shouldn't happen
                {
                    all.Remove(migration);
                    continue;
                }
                //Migration has no dependencies so it can be indexed at order 0.
                if (migration.Requires.Length == 0)
                {
                    indexed.Add(migration, 0);
                    all.Remove(migration);
                    continue;
                }
                //Get all of the dependencies for the migration and resolve their orders
                var indexes = migration.Requires
                    .Select(t => types![t])
                    .Where(indexed.ContainsKey)
                    .Select(t => indexed[t])
                    .ToArray();

                //Not all dependencies have been indexed yet, skip this migration
                if (indexes.Length != migration.Requires.Length) continue;
                
                indexed.Add(migration, indexes.Max() + 1);
                all.Remove(migration);
            }
        }

        //Keep track of the number of iterations incase we hit an infinite loop
        int iteration = 0;
        while (all.Count > 0)
        {
            var count = all.Count; //Keep track of the count before the pass
            Pass(); //Make a pass through and index any new migrations
            //If the count is the same as the number of migrations, we have a circular dependency
            if (count == all.Count) throw new InvalidOperationException("Circular dependency detected.");
            //If we have iterated too many times, throw an exception
            if (iteration++ > MAX_ITERATIONS) throw new InvalidOperationException("Max iterations reached.");
        }
        
        _logger.LogDebug("Finished requirements pass in {iteration} iterations.", iteration);
        return indexed.OrderBy(t => t.Value).Select(t => t.Key).ToArray();
    }

    public async Task Migrate()
    {
        _logger.LogInformation("Starting Migrations...");
        var migrations = GetMigrations();
        foreach (var migration in migrations)
            await migration.Migrate();

        _logger.LogInformation("{count} Migrations Complete.", migrations.Length);
    }
}
