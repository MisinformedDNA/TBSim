namespace TBSim;

public class Phase(PlanetRecord? darkSide, PlanetRecord? mixed, PlanetRecord? lightSide)
{
    public PlanetRecord? DarkSide { get; } = darkSide;
    public PlanetRecord? Mixed { get; } = mixed;
    public PlanetRecord? LightSide { get; } = lightSide;

    public int GetStars() => 
        DarkSide?.Stars ?? 0 + 
        Mixed?.Stars ?? 0 + 
        LightSide?.Stars ?? 0;
}
