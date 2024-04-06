namespace PokeDexSharp.Database.Services.Moves;

public interface IMoveRollup
{
    ICategoryDbService Categories { get; }

    IClassDbService Classes { get; }

    IMethodDbService Methods { get; }

    ITargetDbService Targets { get; }

    IAilmentDbService Ailments { get; }
}

internal class MoveRollup(
    ICategoryDbService category,
    IClassDbService classes,
    IMethodDbService methods,
    ITargetDbService targets,
    IAilmentDbService ailments) : IMoveRollup
{
    public ICategoryDbService Categories => category;

    public IClassDbService Classes => classes;

    public IMethodDbService Methods => methods;

    public ITargetDbService Targets => targets;

    public IAilmentDbService Ailments => ailments;
}
