namespace TBSim.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Single_player_gets_zero_stars()
        {
            var guild = new Guild();
            var player = new Player("");
            player.OverrideGalacticPower(0);
            guild.Players.Add(player);

            var darkSide = new Planet("Mustafar", StarThresholds: [int.MaxValue, int.MaxValue, int.MaxValue], int.MaxValue);
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

            var darkSide = new Planet("Mustafar", StarThresholds: [playerGalacticPower, int.MaxValue, int.MaxValue], int.MaxValue);
            var gameBoard = new GameBoard(darkSide, null!, null!);

            var path = Simulation.FindBestPath(gameBoard, guild);

            Assert.NotNull(path);
            Assert.Equal(1, path.GetStars());
        }

        [Fact]
        public void Single_player_gets_two_stars()
        {
            var guild = new Guild();
            var player = new Player("");
            const int playerGalacticPower = 10_000_000;
            player.OverrideGalacticPower(playerGalacticPower);
            guild.Players.Add(player);

            var darkSide = new Planet("Mustafar", StarThresholds: [0, playerGalacticPower, int.MaxValue], int.MaxValue);
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

            var darkSide = new Planet("Mustafar", StarThresholds: [0, 0, playerGalacticPower], int.MaxValue);
            var gameBoard = new GameBoard(darkSide, null!, null!);

            var path = Simulation.FindBestPath(gameBoard, guild);

            Assert.NotNull(path);
            Assert.Equal(1, path.Phases.Count);
            Assert.Equal(3, path.GetStars());
            Assert.Equal(3, path.Phases[0].GetStars());
        }

        [Fact]
        public void Single_player_gets_one_star_from_two_options()
        {
            var guild = new Guild();
            var player = new Player("");
            const int playerGalacticPower = 10_000_000;
            player.OverrideGalacticPower(playerGalacticPower);
            guild.Players.Add(player);

            var darkSide = new Planet("Mustafar", StarThresholds: [0, int.MaxValue, int.MaxValue], int.MaxValue);
            var other = new Planet("Felucia", StarThresholds: [0, int.MaxValue, int.MaxValue], int.MaxValue);
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

            var darkSide = new Planet("Mustafar", StarThresholds: [0, int.MaxValue, int.MaxValue], int.MaxValue)
            {
                Next = new Planet("Geonosis", StarThresholds: [0, int.MaxValue, int.MaxValue], int.MaxValue)
            };
            var other = new Planet("Felucia", StarThresholds: [0, int.MaxValue, int.MaxValue], int.MaxValue);
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

            var darkSide = new Planet("Mustafar", StarThresholds: [ playerGalacticPower * 6 , int.MaxValue, int.MaxValue], int.MaxValue);
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

            var darkSide = new Planet("Mustafar", StarThresholds: [100_000_000, 200_000_000, int.MaxValue], int.MaxValue);
            var felucia = new Planet("Felucia", StarThresholds: [125_000_000, 250_000_000, int.MaxValue], int.MaxValue);
            var gameBoard = new GameBoard(darkSide, felucia, null!);

            var path = Simulation.FindBestPath(gameBoard, guild);

            Assert.NotNull(path);
            Assert.Equal(2, path.GetStars());
        }
    }
}