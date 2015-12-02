namespace TexasHoldem.AI.SharkPlayer
{
    using System;

    using TexasHoldem.AI.SharkPlayer.Helpers;
    using TexasHoldem.Logic;
    using TexasHoldem.Logic.Extensions;
    using TexasHoldem.Logic.Players;

    public class SharkPlayer : BasePlayer
    {
        public override string Name { get; } = "SharkPlayer_" + Guid.NewGuid();
        
        private int totalOponentBets { get; set; }
        private int numberOfOponentBets { get; set; }

        private int AverageOpponentBet { get; set; }

        public override PlayerAction GetTurn(GetTurnContext context)
        {
            if (context.RoundType == GameRoundType.PreFlop && context.MoneyToCall > 0)
            {
                var playHand = HandStrengthValuation.PreFlop(this.FirstCard, this.SecondCard);
                if (playHand == CardValuationType.Unplayable || playHand == CardValuationType.NotRecommended || playHand == CardValuationType.Risky)
                {
                    if (context.CanCheck)
                    {
                        return PlayerAction.CheckOrCall();
                    }
                    else
                    {
                        return PlayerAction.Fold();
                    }
                }

                if (playHand == CardValuationType.Playable)
                {
                    if (context.MoneyToCall < context.SmallBlind * 6)
                    {
                        var smallBlindsTimes = RandomProvider.Next(4, 7);
                        return PlayerAction.Raise(context.SmallBlind * smallBlindsTimes);
                    }
                    else
                    {
                        return PlayerAction.Fold();
                    }
                }

                if (playHand == CardValuationType.Recommended)
                {
                    if (context.MoneyToCall < context.SmallBlind * 9)
                    {
                        var smallBlindsTimes = RandomProvider.Next(12, 20);
                        return PlayerAction.Raise(context.SmallBlind * smallBlindsTimes);
                    }
                    else
                    {
                        return PlayerAction.CheckOrCall();
                    }
                }

                return PlayerAction.CheckOrCall();
            }

            if (context.RoundType == GameRoundType.PreFlop && context.MoneyToCall == 0)
            {
                var playHand = HandStrengthValuation.PreFlop(this.FirstCard, this.SecondCard);
                if (playHand == CardValuationType.Unplayable)
                {
                    if (context.CanCheck)
                    {
                        return PlayerAction.CheckOrCall();
                    }
                    else
                    {
                        return PlayerAction.Fold();
                    }
                }

                if (playHand == CardValuationType.Risky)
                {
                    var smallBlindsTimes = RandomProvider.Next(6, 10);
                    return PlayerAction.Raise(context.SmallBlind * smallBlindsTimes);
                }

                if (playHand == CardValuationType.Playable)
                {
                    var smallBlindsTimes = RandomProvider.Next(10, 15);
                    return PlayerAction.Raise(context.SmallBlind * smallBlindsTimes);
                }

                if (playHand == CardValuationType.Recommended)
                {
                    var smallBlindsTimes = RandomProvider.Next(15, 20);
                    return PlayerAction.Raise(context.SmallBlind * smallBlindsTimes);
                }

                return PlayerAction.CheckOrCall();
            }

            if (context.RoundType == GameRoundType.Flop)
            {
                var bestHand = HandStrengthValuation.GetBestHand(this.CommunityCards, this.FirstCard, this.SecondCard);
                if ((int) bestHand > (int) HandRankType.Pair)
                {
                    return PlayerAction.Raise(context.CurrentPot*3);
                }
                else
                {
                    return PlayerAction.CheckOrCall();
                }
            }

            if (context.RoundType == GameRoundType.Turn)
            {
                var bestHand = HandStrengthValuation.GetBestHand(this.CommunityCards, this.FirstCard, this.SecondCard);
                if ((int)bestHand > (int)HandRankType.Pair)
                {
                    return PlayerAction.Raise(context.CurrentPot * 3);
                }
                else
                {
                    return PlayerAction.CheckOrCall();
                }
            }

            if (context.RoundType == GameRoundType.River)
            {
                var bestHand = HandStrengthValuation.GetBestHand(this.CommunityCards, this.FirstCard, this.SecondCard);
                if ((int)bestHand > (int)HandRankType.Pair)
                {
                    return PlayerAction.Raise(context.CurrentPot * 4);
                }
                else
                {
                    return PlayerAction.CheckOrCall();
                }
            }

            return PlayerAction.CheckOrCall();
        }
    }
}
