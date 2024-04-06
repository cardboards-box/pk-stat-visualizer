using System.Reflection;

namespace PokeDexSharp.PokeApiLayer;

public interface IPokeApiService
{
    Task<PkPokemon?> Pokemon(int id);

    Task<PkPokemon?> Pokemon(PkResource id);

    Task<PkEncounter[]> Encounter(int id);

    Task<PkEncounter[]> Encounter(PkResource id);

    Task<PkAbility?> Ability(int id);

    Task<PkAbility?> Ability(PkResource id);

    Task<PkGender?> Gender(int id);

    Task<PkGender?> Gender(PkResource id);

    Task<PkLanguage?> Language(int id);

    Task<PkLanguage?> Language(PkResource id);

    Task<PkPokemonForm?> Form(int id);

    Task<PkPokemonForm?> Form(PkResource id);

    Task<PkGame?> Game(int id);

    Task<PkGame?> Game(PkResource id);

    Task<PkType?> Type(int id);

    Task<PkType?> Type(PkResource id);

    Task<T?> Get<T>(int id);

    int[] GetAllIds<T>();
}

public class PokeApiService(IReferenceFileService _ref) : IPokeApiService
{
    public Task<PkPokemon?> Pokemon(int id) => _ref.GetResource<PkPokemon>($"pokemon/{id}");

    public Task<PkPokemon?> Pokemon(PkResource id) => _ref.GetResource<PkPokemon>(id);

    public Task<PkEncounter[]> Encounter(int id) => _ref.GetResources<PkEncounter>($"pokemon/{id}/encounters");

    public Task<PkEncounter[]> Encounter(PkResource id) => _ref.GetResources<PkEncounter>(id);

    public Task<PkAbility?> Ability(int id) => _ref.GetResource<PkAbility>($"ability/{id}");

    public Task<PkAbility?> Ability(PkResource id) => _ref.GetResource<PkAbility>(id);

    public Task<PkGender?> Gender(int id) => _ref.GetResource<PkGender>($"gender/{id}");

    public Task<PkGender?> Gender(PkResource id) => _ref.GetResource<PkGender>(id);

    public Task<PkLanguage?> Language(int id) => _ref.GetResource<PkLanguage>($"language/{id}");

    public Task<PkLanguage?> Language(PkResource id) => _ref.GetResource<PkLanguage>(id);

    public Task<PkPokemonForm?> Form(int id) => _ref.GetResource<PkPokemonForm>($"pokemon-form/{id}");

    public Task<PkPokemonForm?> Form(PkResource id) => _ref.GetResource<PkPokemonForm>(id);

    public Task<PkGame?> Game(int id) => _ref.GetResource<PkGame>($"version/{id}");

    public Task<PkGame?> Game(PkResource id) => _ref.GetResource<PkGame>(id);

    public Task<PkType?> Type(int id) => _ref.GetResource<PkType>($"type/{id}");

    public Task<PkType?> Type(PkResource id) => _ref.GetResource<PkType>(id);

    public Task<T?> Get<T>(int id) => _ref.GetResource<T>(id);

    public int[] GetAllIds<T>() => _ref.GetAllIds<T>();
}
