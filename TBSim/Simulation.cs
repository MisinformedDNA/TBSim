namespace TBSim;

public static class Simulation
{
    private const int PowerIncrement = 1_000_000;

    public static Path? FindBestPath(this GameBoard board, Guild guild)
    {
        if (board.PhaseNumber > 6) return null;

        var players = guild.Players;
        Planet? planet1 = board.DarkSide;
        Planet? planet2 = board.Mixed;
        Planet? planet3 = board.LightSide;

        if (planet1 is null && planet2 is null && planet3 is null)
            return null;

        var bestPath = new Path();

        var planet1SignificantPowerTiers = planet1?.GetSignificantPowerTiers() ?? [0];
        var planet2SignificantPowerTiers = planet2?.GetSignificantPowerTiers() ?? [0];
        var planet3SignificantPowerTiers = planet3?.GetSignificantPowerTiers() ?? [0];

        foreach (var powerTier1 in planet1SignificantPowerTiers)
        {
            foreach (var powerTier2 in planet2SignificantPowerTiers)
            {
                foreach (var powerTier3 in planet3SignificantPowerTiers)
                {
                    int galacticPower = players.Sum(x => x.GalacticPower);

                    if (powerTier1 > 0 && planet1 is null) continue;
                    if (powerTier2 > 0 && planet2 is null) continue;
                    if (powerTier3 > 0 && planet3 is null) continue;

                    var power1 = GetPower(powerTier1);
                    var power2 = GetPower(powerTier2);
                    var power3 = GetPower(powerTier3);

                    int GetPower(int powerTier)
                    {
                        if (powerTier == 0) return 0;

                        powerTier = Math.Min(powerTier, galacticPower);

                        galacticPower -= powerTier;
                        return powerTier;
                    }

                    planet1?.DeployToOps(ZoneId.Zone1, players);
                    planet1?.DeployUnits(power1);
                    planet2?.DeployUnits(power2);
                    planet3?.DeployUnits(power3);

                    var currentPath = new Path();
                    currentPath.AddPhase(board.GetPhaseSnapshot());

                    board.GoToNextPhase();
                    var continuingPath = board.FindBestPath(guild);
                    if (continuingPath is not null)
                        currentPath.Append(continuingPath);

                    if (currentPath.GetStars() > bestPath.GetStars()) // compare path length. Discard when length is longer?
                    {
                        bestPath = currentPath;
                    }

                    board.Undo();

                    planet1?.Undo();
                    planet2?.Undo();
                    planet3?.Undo();
                }
            }
        }

        return bestPath;

    }

    public static Planet FindBestPath(this Planet planet, Guild guild)
    {
        planet.DeployUnits(guild.Players[0].GalacticPower);
        return planet;
    }

}
