namespace PokeDexSharp;

using PokeApiLayer;

public static class DiExtensions
{
    public static IDependencyResolver AddPokeApi(this IDependencyResolver resolver)
    {
        return resolver
            .Transient<IReferenceFileService, ReferenceFileService>()
            .Transient<IPokeApiService, PokeApiService>();
    }
}
