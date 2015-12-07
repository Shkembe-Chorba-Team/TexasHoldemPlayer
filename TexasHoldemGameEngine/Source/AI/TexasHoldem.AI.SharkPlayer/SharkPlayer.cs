namespace TexasHoldem.AI.SharkPlayer
{
    using System;

    using TexasHoldem.AI.SharkPlayer.Helpers;
    using TexasHoldem.Logic.Players;
    using TexasHoldem.AI.SharkPlayer.HandOptions;

    /// <summary>
    /// A Shark Poker Player.
    /// </summary>
    public class SharkPlayer : BasePlayer
    {
        /// <summary>
        /// Name of the player.
        /// </summary>
        public override string Name { get; } = "SharkPlayer_" + Guid.NewGuid();

        /// <summary>
        /// An int that count the total opponent's bets.
        /// </summary>
        private int TotalOponentBets { get; set; }

        /// <summary>
        /// An int that count the total number of opponent's bets.
        /// </summary>
        private int NumberOfOponentBets { get; set; }

        /// <summary>
        /// An int that count the average opponent's bets.
        /// </summary>
        private int AverageOpponentBet { get; set; }

        /// <summary>
        /// Base GetTurn method. It contains AI's logic and returns a PlayerAction.
        /// </summary>
        /// <param name="context">Get Turn Context parameter.</param>
        /// <returns>Player action.</returns>
        public override PlayerAction GetTurn(GetTurnContext context)
        {
            var playHand = HandStrengthValuation.PreFlop(this.FirstCard, this.SecondCard);
            var bestHand = HandStrengthValuation.GetBestHand(this.CommunityCards, this.FirstCard, this.SecondCard);
            var bestHandOnTable = TableStrengthOpportunities.GetBestPossibleHand(this.CommunityCards, this.FirstCard, this.SecondCard);

            var handsHolder = new HandsHolder(playHand, bestHand, bestHandOnTable);

            var playerAction = PlayerActionFactory.GetPlayerAction(context, handsHolder);

            return playerAction;
        }
    }
}