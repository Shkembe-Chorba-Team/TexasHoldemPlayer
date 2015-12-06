namespace TexasHoldem.Tests.GameSimulations.GameSimulators
{
    using AI.SharkPlayer;
    using TexasHoldem.AI.DummyPlayer;
    using TexasHoldem.AI.SmartPlayer;
    using TexasHoldem.Logic.Players;

    internal class SharkVsAlwaysCallPlayerSimulator : BaseGameSimulator
    {
        private readonly IPlayer firstPlayer = new SharkPlayer();

        private readonly IPlayer secondPlayer = new AlwaysCallDummyPlayer();

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
