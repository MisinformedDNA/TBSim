namespace TBSim
{
    public record Planet(string Name, int[] StarThresholds, int OperationZoneBonus)
    {
        public List<Operation> Operations { get; } = [];
        public int GalacticPower { get; private set; }
        public static Planet Impossible { get; } = new("Impossible", StarThresholds: [int.MaxValue, int.MaxValue, int.MaxValue], int.MaxValue);
        public Planet? Next { get; init; }

        public void DeployUnits(int galacticPower) => GalacticPower += galacticPower;

        public int GetStars()
        {
            if (GalacticPower >= StarThresholds[2]) return 3;
            if (GalacticPower >= StarThresholds[1]) return 2;
            if (GalacticPower >= StarThresholds[0]) return 1;
            return 0;
        }

        public bool IsComplete() => GetStars() > 0;

        public void ResetPlanet()
        {
            GalacticPower = 0;
        }
    }
}

//class Simulation
//{
//    static void Main()
//    {
//        // Initialize guild, players, and units (you need to fill in the details)

//        // Initialize planets, operations, and their details (you need to fill in the details)

//        // Run simulation for each phase
//        List<Planet> planets = new List<Planet>(); // Fill in the planets list
//        int phase = 1;
//        int maxStars = 0;

//        // Find the best strategy for each planet in the current phase
//        foreach (var planet in planets)
//        {
//            int stars = FindBestStrategy(planet, phase, new Guild(), 0);
//            maxStars += stars;
//        }

//        Console.WriteLine("Maximum Stars: " + maxStars);
//    }

//    static int FindBestStrategy(Planet planet, int phase, Guild guild, int currentStar)
//    {
//        // Base case: If the planet is already complete or no more stars can be earned
//        if (currentStar >= planet.StarThresholds.Length || currentStar > 2)
//            return 0;

//        // Simulate deploying units to the current planet
//        int maxStars = 0;

//        foreach (var player in guild.Players)
//        {
//            // Simulate deploying units for the current player
//            // You need to implement the deployment logic based on your game rules

//            // Recursive call for the next planet and update the current star count
//            int stars = FindBestStrategy(planet, phase + 1, guild, currentStar + 1);

//            // Update the maximum stars based on the current player's deployment
//            maxStars = Math.Max(maxStars, stars);
//        }

//        return maxStars;
//    }
//}
