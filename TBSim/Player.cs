namespace TBSim;

public record Player(string Name)
{
    private int? _galacticPower;

    public List<Unit> Units { get; } = [];

    public int GalacticPower => _galacticPower ?? Units.Sum(x => x.GalacticPower);

    public void OverrideGalacticPower(int power) => _galacticPower = power;
}
