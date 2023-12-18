namespace TBSim;

public class GameBoard(Planet darkSide, Planet mixed, Planet lightSide)
{
    public Planet? DarkSide { get; set; } = darkSide;
    public Planet? LightSide { get; set; } = lightSide;
    public Planet? Mixed { get; set; } = mixed;
    public Stack<(Planet? DarkSide, Planet? Mixed, Planet? LightSide)> Changes { get; } = [];

    public int PhaseNumber { get; private set; } = 1;

    public void GoToNextPhase()
    {
        Changes.Push((DarkSide, Mixed, LightSide));

        DarkSide = GetPlanetForNextPhase(DarkSide);
        LightSide = GetPlanetForNextPhase(LightSide);
        Mixed = GetPlanetForNextPhase(Mixed);
        PhaseNumber++;
    }

    public void Undo()
    {
        var previousPlanets = Changes.Pop();

        DarkSide = previousPlanets.DarkSide;
        LightSide = previousPlanets.LightSide;
        Mixed = previousPlanets.Mixed;
        PhaseNumber--;
    }

    public static Planet? GetPlanetForNextPhase(Planet? planet)
    {
        return planet is null
            ? null
            : planet.IsComplete()
            ? planet.Next
            : planet;
    }

    public Phase GetPhaseSnapshot()
    {
        var darkSideRecord = DarkSide is null ? null : new PlanetRecord(DarkSide);
        var mixedRecord = Mixed is null ? null : new PlanetRecord(Mixed);
        var lightSideRecord = LightSide is null ? null : new PlanetRecord(LightSide);
        return new Phase(darkSideRecord, mixedRecord, lightSideRecord);
    }
}
