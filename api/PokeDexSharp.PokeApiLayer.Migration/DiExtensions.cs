namespace PokeDexSharp;

using PokeApiLayer.Migration;
using PokeApiLayer.Migration.Migrations;
using PokeApiLayer.Migration.Migrations.Games;
using PokeApiLayer.Migration.Migrations.Moves;

public static class DiExtensions
{
    public static IDependencyResolver AddPokeMigration(this IDependencyResolver resolver)
    {
        return resolver
            .Transient<IMigrationService, MigrationService>()
            .Transient<ICommonService, CommonService>()

            .Transient<IMigration, LanguageMigration>()
            .Transient<IMigration, ElementMigration>()

            .Transient<IMigration, MoveCategoryMigration>()
            .Transient<IMigration, MoveClassMigration>()
            .Transient<IMigration, MoveMethodMigration>()
            .Transient<IMigration, MoveTargetMigration>()

            .Transient<IMigration, GameGenerationMigration>()
            .Transient<IMigration, GameGroupMapMigration>()
            .Transient<IMigration, GameGroupMigration>()
            .Transient<IMigration, GameLocationMigration>()
            .Transient<IMigration, GameMigration>()
            .Transient<IMigration, GameRegionMigration>()

            ;
    }
}
