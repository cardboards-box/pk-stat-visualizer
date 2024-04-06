namespace PokeDexSharp.Database.Services.Moves;

public interface IMoveRollup
{
    ICategoryDbService Categories { get; }

    IClassDbService Classes { get; }

    IMethodDbService Methods { get; }

    ITargetDbService Targets { get; }
}

internal class MoveRollup(
    ICategoryDbService category,
    IClassDbService classes,
    IMethodDbService methods,
    ITargetDbService targets) : IMoveRollup
{
    public ICategoryDbService Categories => category;

    public IClassDbService Classes => classes;

    public IMethodDbService Methods => methods;

    public ITargetDbService Targets => targets;
}
