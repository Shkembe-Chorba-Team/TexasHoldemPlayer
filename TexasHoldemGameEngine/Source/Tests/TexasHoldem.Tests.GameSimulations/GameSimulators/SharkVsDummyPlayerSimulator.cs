namespace TexasHoldem.Tests.GameSimulations.GameSimulators
{
    using TexasHoldem.AI.SharkPlayer;
    using TexasHoldem.AI.DummyPlayer;
    using TexasHoldem.Logic.Players;

    internal class SharkVsDummyPlayerSimulator : BaseGameSimulator
    {
        private readonly IPlayer firstPlayer = new SharkPlayer();

        private readonly IPlayer secondPlayer = new DummyPlayer();

        protected override IPlayer GetFirstPlayer()
        {
            return this.firstPlayer;
        }

        protected override IPlayer GetSecondPlayer()
        {
            return this.secondPlayer;
        }
    }
}
