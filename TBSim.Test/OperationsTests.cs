using AutoFixture;
using AutoFixture.Dsl;

namespace TBSim.Test;

public class OperationsTests
{
    private readonly Fixture _fixture = new();
    private const int OpsZoneBonus = 10_000_000;
    private const int TestGalacticPower = 1_000_000;


    [Fact]
    public void Operation_is_used_to_get_three_stars()
    {
        var guild = new Guild();
        var player = new Player("")
        {
            Units = 
            [
                new Unit("", 10_000_000, Level.SevenStars)
            ]
        };
        guild.Players.Add(player);

        var darkSide = BuildPlanet()
            .With(x => x.StarThresholds, [0, 0, TestGalacticPower + OpsZoneBonus])
            .With(x => x.Operations, _fixture
                .Build<Operations>()
                .With(x => x.Zone1, new OperationZone([]))
                .Create())
            .Create();
        var gameBoard = new GameBoard(darkSide, null!, null!);

        var path = Simulation.FindBestPath(gameBoard, guild);

        Assert.NotNull(path);
        Assert.Equal(1, path.Phases.Count);
        Assert.Equal(3, path.GetStars());
    }

    private IPostprocessComposer<Planet> BuildPlanet()
    {
        return _fixture.Build<Planet>()
            .With(x => x.OperationZoneBonus, OpsZoneBonus)
            .Without(x => x.Next);
    }
}