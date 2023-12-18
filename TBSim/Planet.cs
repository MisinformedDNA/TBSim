namespace TBSim;

public record Planet(string Name, int[] StarThresholds, int OperationZoneBonus)
{
    public List<Operation> Operations { get; } = [];
    public int GalacticPower { get; private set; }
    public Stack<GameAction> GameActions { get; } = [];
    public Planet? Next { get; init; }

    public void DeployUnits(int galacticPower)
    {
        GalacticPower += galacticPower;
        GameActions.Push(new GuildDeploymentAction(galacticPower));
    }


    public int GetStars()
    {
        if (GalacticPower >= StarThresholds[2]) return 3;
        if (GalacticPower >= StarThresholds[1]) return 2;
        if (GalacticPower >= StarThresholds[0]) return 1;
        return 0;
    }

    public bool IsComplete() => GetStars() > 0;

    public IEnumerable<int> GetSignificantPowerTiers()
    {
        List<int> tiers = [StarThresholds[2], StarThresholds[2] - 1, StarThresholds[1], StarThresholds[1] - 1, StarThresholds[0], StarThresholds[0] - 1];
        foreach (var tier in tiers.Where(x => x > GalacticPower))
        {
            yield return tier;
        }

        yield return 0;
    }

    public void Undo()
    {
        var action = GameActions.Pop();
        if (action is GuildDeploymentAction deploymentAction)
            GalacticPower -= deploymentAction.GalacticPower;
    }
}

public record PlanetRecord(string Name, int Stars)
{
    public PlanetRecord(Planet planet)
        : this(planet.Name, planet.GetStars())
    {
    }
}
