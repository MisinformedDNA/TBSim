namespace TBSim;

public static class Simulation
{
    private const int PowerIncrement = 1_000_000;

    //const int IncrementAmount = 
    public static Path? FindBestPath(this GameBoard board, Guild guild)
    {
        Planet? planet1 = board.DarkSide;
        Planet? planet2 = board.Mixed;
        Planet? planet3 = board.LightSide;

        if (planet1 is null && planet2 is null && planet3 is null)
            return null;

        var best = new Phase(null, null, null);
        int powerLevel = guild.Players[0].GalacticPower;

        for (int power1 = 0; power1 <= powerLevel; power1 += PowerIncrement)
        {
            for (int power2 = 0; power2 <= powerLevel - power1; power2 += PowerIncrement)
            {
                int power3 = powerLevel - power1 - power2;

                planet1?.ResetPlanet();
                planet2?.ResetPlanet();
                planet3?.ResetPlanet();

                planet1?.DeployUnits(power1);
                planet2?.DeployUnits(power2);
                planet3?.DeployUnits(power3);



                var potentialPhase = new Phase(planet1, planet2, planet3);

                if (best is null)
                {
                    best = potentialPhase;
                    continue;
                }

                if (potentialPhase.GetStars() > best.GetStars())
                {
                    best = potentialPhase;
                }
            }
        }

        var path = new Path();
        path.AddPhase(best);

        if (board.PhaseNumber < 6)
        {
            board.GoToNextPhase();
            var continuingPath = board.FindBestPath(guild);
            if (continuingPath is not null)
                path.Append(continuingPath);
        }

        return path;
    }

    public static Planet FindBestPath(this Planet planet, Guild guild)
    {
        planet.DeployUnits(guild.Players[0].GalacticPower);
        return planet;
    }

}
