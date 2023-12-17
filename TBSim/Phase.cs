namespace TBSim;

public class Phase(Planet? darkSide, Planet? mixed, Planet? lightSide)
{
    public Planet? DarkSide { get; } = darkSide;
    public Planet? Mixed { get; } = mixed;
    public Planet? LightSide { get; } = lightSide;

    public int GetStars() => 
        DarkSide?.GetStars() ?? 0 + 
        Mixed?.GetStars() ?? 0 + 
        LightSide?.GetStars() ?? 0;
}
