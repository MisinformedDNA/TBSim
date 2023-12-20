namespace TBSim;

public record Operations()
{
    private readonly Dictionary<ZoneId, OperationZone> _zones = [];

    private readonly ZoneId[] _zoneIds = [ZoneId.Zone1, ZoneId.Zone2, ZoneId.Zone3, ZoneId.Zone4, ZoneId.Zone5, ZoneId.Zone6];

    public OperationZone Zone1
    {
        get => _zones[ZoneId.Zone1];
        set => _zones[ZoneId.Zone1] = value;
    }

    public OperationZone Zone2
    {
        get => _zones[ZoneId.Zone2];
        set => _zones[ZoneId.Zone2] = value;
    }

    public OperationZone Zone3
    {
        get => _zones[ZoneId.Zone3];
        set => _zones[ZoneId.Zone3] = value;
    }

    public OperationZone Zone4
    {
        get => _zones[ZoneId.Zone4];
        set => _zones[ZoneId.Zone4] = value;
    }

    public OperationZone Zone5
    {
        get => _zones[ZoneId.Zone5];
        set => _zones[ZoneId.Zone5] = value;
    }

    public OperationZone Zone6
    {
        get => _zones[ZoneId.Zone6];
        set => _zones[ZoneId.Zone6] = value;
    }

    public OperationZone GetZone(ZoneId zone) => _zones[zone];

    public IEnumerable<OperationZone> GetAllZones() => _zones.Values;
}

public class OperationZone
{
    private readonly OperationRequirement[] _requirements;

    public OperationZone(RequiredUnit[] requiredUnits) => 
        _requirements = requiredUnits
            .Select(x => new OperationRequirement(x))
            .ToArray();

    public void Deploy(Player player, string unitName)
    {
        var requirement = GetAvailableMatch(unitName)
            ?? throw new InvalidOperationException("Unit is not required or has been already been deployed.");
        var playerUnit = player.Units.FirstOrDefault(x => x.Name == unitName)
            ?? throw new InvalidOperationException("Player does not have unit.");
        if (playerUnit.Level < requirement.Unit.MinimumLevel)
            throw new InvalidOperationException("Unit does not meet the requirements.");

        requirement.FillUnit(player);
    }

    public bool IsComplete() => _requirements.All(x => x.FilledBy != null);

    public OperationRequirement? GetAvailableMatch(string unitName) =>
        _requirements.FirstOrDefault(x => !x.IsFilled && x.Unit.Name == unitName);

    public bool HasAvailableMatch(string unitName) => _requirements
        .Any(x => !x.IsFilled && x.Unit.Name == unitName);
}

public class OperationRequirement(RequiredUnit unit)
{
    public RequiredUnit Unit { get; } = unit;
    public Player? FilledBy { get; private set; }

    public void FillUnit(Player player)
    {
        ArgumentNullException.ThrowIfNull(nameof(player));

        if (IsFilled)
            throw new InvalidOperationException("Unit already set.");

        FilledBy = player;
    }

    public bool IsFilled => FilledBy != null;

    public void RemoveUnit()
    {
        if (!IsFilled)
            throw new InvalidOperationException("There is no unit to remove.");

        FilledBy = null;
    }
}

public enum ZoneId
{
    Zone1,
    Zone2,
    Zone3,
    Zone4,
    Zone5,
    Zone6
}

public record RequiredUnit(string Name, Level MinimumLevel);

public enum Level
{
    BelowSevenStars,
    SevenStars,
    R5,
    R6,
    R7,
    R8,
    R9
}
