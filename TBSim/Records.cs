namespace TBSim;

public record Guild
{
    public List<Player> Players { get; } = [];
}

public record Unit(string Name, int GalacticPower, Level Level);

public enum Level
{
    SevenStars,
    R5,
    R6,
    R7,
    R8,
    R9
}

public record Operation(string Name)
{
    public List<string> RequiredUnits { get; } = [];
}
