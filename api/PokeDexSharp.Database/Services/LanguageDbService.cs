namespace PokeDexSharp.Database.Services;

using Models;

public interface ILanguageDbService : IKeyedOrm<Language>
{

}

public class LanguageDbService(IOrmService orm) : KeyedCacheOrm<Language>(orm), ILanguageDbService
{

}
