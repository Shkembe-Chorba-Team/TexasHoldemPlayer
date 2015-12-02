namespace TexasHoldem.Tests.GameSimulations.GameSimulators
{
    using TexasHoldem.AI.SharkPlayer;
    using TexasHoldem.AI.AllInPlayer;
    using TexasHoldem.Logic.Players;

    internal class SharkVsAllInPlayerSimulator : BaseGameSimulator
    {
        private readonly IPlayer firstPlayer = new SharkPlayer();

        private readonly IPlayer secondPlayer = new AllInPlayer();

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
