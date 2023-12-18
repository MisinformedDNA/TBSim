using AutoFixture;
using AutoFixture.Dsl;

namespace TBSim.Test
{
    public class GalacticPowerTests
    {
        private readonly Fixture _fixture = new();

        [Fact]
        public void Single_player_gets_zero_stars()
        {
            var guild = new Guild();
            var player = new Player("");
            player.OverrideGalacticPower(0);
            guild.Players.Add(player);

            var darkSide = CreatePlanet(starThresholds: [1, 1, 1]);
            var gameBoard = new GameBoard(darkSide, null!, null!);

            var path = Simulation.FindBestPath(gameBoard, guild);

            Assert.NotNull(path);
            Assert.Equal(0, path.GetStars());
        }

        [Fact]
        public void Single_player_gets_one_star()
        {
            var guild = new Guild();
            var player = new Player("");
            const int playerGalacticPower = 10_000_000;
            player.OverrideGalacticPower(playerGalacticPower);
            guild.Players.Add(player);

            var darkSide = CreatePlanet(starThresholds: [playerGalacticPower, int.MaxValue, int.MaxValue]);
            var gameBoard = new GameBoard(darkSide, null!, null!);

            var path = Simulation.FindBestPath(gameBoard, guild);

            Assert.NotNull(path);
            Assert.Equal(1, path.GetStars());
        }

        [Fact]
        public void Single_player_gets_one_star_in_one_phase()
        {
            var guild = new Guild();
            var player = new Player("");
            const int playerGalacticPower = 10_000_000;
            player.OverrideGalacticPower(playerGalacticPower);
            guild.Players.Add(player);

            var darkSide = CreatePlanet(starThresholds: [playerGalacticPower, int.MaxValue, int.MaxValue]);
            var gameBoard = new GameBoard(darkSide, null!, null!);

            var path = Simulation.FindBestPath(gameBoard, guild);

            Assert.NotNull(path);
            Assert.Equal(1, path.GetStars());
            Assert.Equal(1, path.Phases.Count);
        }

        [Fact]
        public void Single_player_gets_two_stars()
        {
            var guild = new Guild();
            var player = new Player("");
            const int playerGalacticPower = 10_000_000;
            player.OverrideGalacticPower(playerGalacticPower);
            guild.Players.Add(player);

            var darkSide = CreatePlanet(starThresholds: [0, playerGalacticPower, int.MaxValue]);
            var gameBoard = new GameBoard(darkSide, null!, null!);

            var path = Simulation.FindBestPath(gameBoard, guild);

            Assert.NotNull(path);
            Assert.Equal(1, path.Phases.Count);
            Assert.Equal(2, path.GetStars());
            Assert.Equal(2, path.Phases[0].GetStars());
        }

        [Fact]
        public void Single_player_gets_three_stars()
        {
            var guild = new Guild();
            var player = new Player("");
            const int playerGalacticPower = 10_000_000;
            player.OverrideGalacticPower(playerGalacticPower);
            guild.Players.Add(player);

            var darkSide = CreatePlanet(starThresholds: [0, 0, playerGalacticPower]);
            var gameBoard = new GameBoard(darkSide, null!, null!);

            var path = Simulation.FindBestPath(gameBoard, guild);

            Assert.NotNull(path);
            Assert.Equal(1, path.Phases.Count);
            Assert.Equal(3, path.GetStars());
        }

        [Fact]
        public void Single_player_gets_one_star_from_two_options()
        {
            var guild = new Guild();
            var player = new Player("");
            const int playerGalacticPower = 10_000_000;
            player.OverrideGalacticPower(playerGalacticPower);
            guild.Players.Add(player);

            var darkSide = CreatePlanet(starThresholds: [0, int.MaxValue, int.MaxValue]);
            var other = CreatePlanet(starThresholds: [0, int.MaxValue, int.MaxValue]);
            var gameBoard = new GameBoard(darkSide, other, null!);

            var path = Simulation.FindBestPath(gameBoard, guild);

            Assert.NotNull(path);
            Assert.Equal(1, path.Phases.Count);
            Assert.Equal(1, path.GetStars());
            Assert.Equal(1, path.Phases[0].GetStars());
        }

        [Fact]
        public void Single_player_gets_two_total_stars_from_two_planets()
        {
            var guild = new Guild();
            var player = new Player("");
            const int playerGalacticPower = 10_000_000;
            player.OverrideGalacticPower(playerGalacticPower);
            guild.Players.Add(player);

            var darkSide = BuildPlanet()
                .With(x => x.StarThresholds, [0, int.MaxValue, int.MaxValue])
                .With(x => x.Next, CreatePlanet(starThresholds: [0, int.MaxValue, int.MaxValue]))
                .Create();
            var other = CreatePlanet(starThresholds: [0, int.MaxValue, int.MaxValue]);
            var gameBoard = new GameBoard(darkSide, other, null!);

            var path = Simulation.FindBestPath(gameBoard, guild);

            Assert.NotNull(path);
            Assert.Equal(2, path.Phases.Count);
            Assert.Equal(2, path.GetStars());
            Assert.Equal(1, path.Phases[0].GetStars());
            Assert.Equal(1, path.Phases[1].GetStars());
        }

        [Fact]
        public void Single_player_gets_one_star_in_phase_6()
        {
            var guild = new Guild();
            var player = new Player("");
            const int playerGalacticPower = 10_000_000;
            player.OverrideGalacticPower(playerGalacticPower);
            guild.Players.Add(player);

            var darkSide = CreatePlanet(starThresholds: [playerGalacticPower * 6, int.MaxValue, int.MaxValue]);
            var gameBoard = new GameBoard(darkSide, null!, null!);

            var path = Simulation.FindBestPath(gameBoard, guild);

            Assert.NotNull(path);
            Assert.Equal(6, path.Phases.Count);
            Assert.Equal(1, path.GetStars());
            Assert.Equal(1, path.Phases[5].GetStars());
        }


        [Fact]
        public void Single_player_gets_two_total_stars_on_two_planets()
        {
            var guild = new Guild();
            var player = new Player("");
            const int playerGalacticPower = 99_000_000;
            player.OverrideGalacticPower(playerGalacticPower);
            guild.Players.Add(player);

            var darkSide = CreatePlanet(starThresholds: [100_000_000, 200_000_000, int.MaxValue]);
            var felucia = CreatePlanet(starThresholds: [125_000_000, 250_000_000, int.MaxValue]);
            var gameBoard = new GameBoard(darkSide, felucia, null!);

            var path = Simulation.FindBestPath(gameBoard, guild);

            Assert.NotNull(path);
            Assert.Equal(2, path.GetStars());
        }

        private Planet CreatePlanet(int[] starThresholds)
        {
            return BuildPlanet()
                .With(x => x.StarThresholds, starThresholds)
                .Create();
        }

        private IPostprocessComposer<Planet> BuildPlanet()
        {
            return _fixture.Build<Planet>()
                .With(x => x.OperationZoneBonus, 0)
                .Without(x => x.Next);
        }
    }
}