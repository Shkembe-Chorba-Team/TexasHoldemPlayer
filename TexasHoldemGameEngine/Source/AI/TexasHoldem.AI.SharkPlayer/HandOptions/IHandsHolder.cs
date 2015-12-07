using TexasHoldem.AI.SharkPlayer.Helpers;
using TexasHoldem.Logic;

namespace TexasHoldem.AI.SharkPlayer.HandOptions
{
    public interface IHandsHolder
    {
        HandRankType BestHand { get; set; }

        HandRankType BestHandOnTable { get; set; }

        CardValuationType PlayHand { get; set; }
    }
}