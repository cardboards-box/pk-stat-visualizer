namespace PokeDexSharp;

using Database.Services;
using Database.Services.Games;
using Database.Services.Moves;
using Models;

public static class DiExtensions
{
    public static IDependencyResolver AddDatabase(this IDependencyResolver resolver)
    {
        return resolver
            .Transient<IDbService, DbService>()
            //Moves
            .Transient<IMoveRollup, MoveRollup>()
            .Transient<ICategoryDbService, CategoryDbService>()
            .Transient<IMethodDbService, MethodDbService>()
            .Transient<ITargetDbService, TargetDbService>()
            .Transient<IClassDbService, ClassDbService>()
            //Games
            .Transient<IGameRollup, GameRollup>()
            .Transient<IRegionDbService, RegionDbService>()
            .Transient<IGenerationDbService, GenerationDbService>()
            .Transient<ILocationDbService, LocationDbService>()
            .Transient<IGameDbService, GameDbService>()
            .Transient<IGroupDbService, GroupDbService>()
            .Transient<IMapDbService, MapDbService>()

            .Transient<ILanguageDbService, LanguageDbService>()
            .Transient<IElementDbService, ElementDbService>()
            
            .Model<Move>()
            .Model<MoveCategory>()
            .Model<MoveClass>()
            .Model<MoveMethod>()
            .Model<MoveTarget>()

            .Model<Game>()
            .Model<GameGeneration>()
            .Model<GameRegion>()
            .Model<GameGroup>()
            .Model<GameGroupMap>()
            .Model<GameLocation>()

            .Model<Element>()
            .Model<Language>()
            
            .Type<ExtendedLocalization>("pk_extended_localization")
            .Type<ExtendedVersionLocalization>("pk_extended_version_localization")
            .Type<Localization>("pk_localization")
            .Type<VersionLocalization>("pk_version_localization");
    }
}
