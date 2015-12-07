namespace TexasHoldem.AI.SharkPlayer.HandOptions
{
    using Helpers;
    using Logic;

    public class HandsHolder : IHandsHolder
    {
        public HandsHolder(CardValuationType playHand, HandRankType bestHand, HandRankType bestHandOnTable)
        {
            this.PlayHand = playHand;
            this.BestHand = bestHand;
            this.BestHandOnTable = bestHandOnTable;
        }

        public CardValuationType PlayHand { get; set; }

        public HandRankType BestHand { get; set; }

        public HandRankType BestHandOnTable { get; set; }
    }
}
