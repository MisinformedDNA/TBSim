namespace TBSim.Test;

public class OperationZoneTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Zone_with_no_requirements_is_completed()
    {
        var zone = new OperationZone([]);
        zone.IsComplete().Should().BeTrue();
    }

    [Fact]
    public void Zone_with_requirements_is_not_completed()
    {
        var zone = new OperationZone([new RequiredUnit("", Level.SevenStars)]);
        zone.IsComplete().Should().BeFalse();
    }

    [Fact]
    public void Requirement_is_not_valid()
    {
        var unit = _fixture.Create<RequiredUnit>();
        var zone = new OperationZone([unit]);
        var player = _fixture.Create<Player>();

        Action action = () => zone.Deploy(player, _fixture.Create<string>());

        action.Should().Throw<InvalidOperationException>().WithMessage("Unit is not required or has been already been deployed.");
    }

    [Fact]
    public void Player_does_not_have_unit_to_deploy()
    {
        var unit = _fixture.Create<RequiredUnit>();
        var zone = new OperationZone([unit]);
        var player = _fixture.Create<Player>();

        Action action = () => zone.Deploy(player, unit.Name);

        action.Should().Throw<InvalidOperationException>().WithMessage("Player does not have unit.");
    }


    [Fact]
    public void Unit_level_is_too_low()
    {
        var unit = _fixture.Build<RequiredUnit>()
            .With(x => x.MinimumLevel, Level.SevenStars)
            .Create();
        var zone = new OperationZone([unit]);
        var player = _fixture.Build<Player>()
            .With(x => x.Units, [new Unit(unit.Name, 0, Level.BelowSevenStars)])
            .Create();

        Action action = () => zone.Deploy(player, unit.Name);

        action.Should().Throw<InvalidOperationException>().WithMessage("Unit does not meet the requirements.");
    }

    [Fact]
    public void Player_can_deploy_unit()
    {
        var unit = _fixture.Create<RequiredUnit>();
        var zone = new OperationZone([unit]);
        var player = _fixture.Build<Player>()
            .With(x => x.Units, [new Unit(unit.Name, 0, Level.SevenStars)])
            .Create();

        zone.Deploy(player, unit.Name);

        zone.IsComplete().Should().BeTrue();
    }
}
