namespace PokeDexSharp.PokeApiLayer.Migration;

public interface IMigration
{
    Type Target { get; }

    Type[] Requires { get; }

    string Name { get; }

    Task Migrate();
}
