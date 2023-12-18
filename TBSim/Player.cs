namespace TBSim;

public record Guild
{
    public List<Player> Players { get; } = [];
}

public record Player(string Name)
{
    private int? _galacticPower;

    public List<Unit> Units { get; init; } = [];

    public int GalacticPower => _galacticPower ?? Units.Sum(x => x.GalacticPower);

    public void OverrideGalacticPower(int power) => _galacticPower = power;
}
