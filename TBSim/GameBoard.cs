namespace TBSim;

public class GameBoard(Planet darkSide, Planet mixed, Planet lightSide)
{
    public Planet? DarkSide { get; set; } = darkSide;
    public Planet? LightSide { get; set; } = lightSide;
    public Planet? Mixed { get; set; } = mixed;

    public int PhaseNumber { get; private set; } = 1;

    public void GoToNextPhase()
    {
        DarkSide = GetPlanetForNextPhase(DarkSide);
        LightSide = GetPlanetForNextPhase(LightSide);
        Mixed = GetPlanetForNextPhase(Mixed);
        PhaseNumber++;
    }

    public static Planet? GetPlanetForNextPhase(Planet? planet)
    {
        return planet is null
            ? null
            : planet.IsComplete()
            ? planet.Next
            : planet;
    }
}
