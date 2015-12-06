namespace TexasHoldem.Tests.GameSimulations
{
    using System;

    using TexasHoldem.Tests.GameSimulations.GameSimulators;

    public static class Program
    {
        public static void Main()
        {
            SimulateGames(new SharkVsAlwaysCallPlayerSimulator());
            //SimulateGames(new SmartVsDummyPlayerSimulator());
            //SimulateGames(new SmartVsSmartPlayerSimulator());
            //SimulateGames(new AlwaysCallPlayersGameSimulator());
            SimulateGames(new SharkVsAllInPlayerSimulator());
            SimulateGames(new SharkVsSmartPlayerSimulator());
            SimulateGames(new SharkVsDummyPlayerSimulator());

        }

        private static void SimulateGames(IGameSimulator gameSimulator)
        {
            Console.WriteLine($"Running {gameSimulator.GetType().Name}...");

            var simulationResult = gameSimulator.Simulate(5000);

            Console.WriteLine(simulationResult.SimulationDuration);
            Console.WriteLine($"Total games: {simulationResult.FirstPlayerWins:0,0} - {simulationResult.SecondPlayerWins:0,0}");
            Console.WriteLine($"Hands played: {simulationResult.HandsPlayed:0,0}");
            Console.WriteLine(new string('=', 75));
        }
    }
}
