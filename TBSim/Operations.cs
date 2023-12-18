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

public record OperationZone(RequiredUnit[] RequiredUnits)
{
    public List<PlacedUnit> PlacedUnits { get; set; } = new(15);

    public bool IsComplete() => RequiredUnits.Length == PlacedUnits.Count;
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
    SevenStars,
    R5,
    R6,
    R7,
    R8,
    R9
}

public record PlacedUnit(Player Player, Unit Unit);