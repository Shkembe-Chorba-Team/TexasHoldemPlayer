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
                    if (context.MoneyToCall < context.SmallBlind * 3)
                    {
                        var smallBlindsTimes = RandomProvider.Next(2, 4);
                        return PlayerAction.Raise(context.SmallBlind * smallBlindsTimes);
                    }
                    else
                    {
                        return PlayerAction.Fold();
                    }
                }

                if (playHand == CardValuationType.Recommended)
                {
                    if (context.MoneyToCall < context.SmallBlind * 7)
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
                    var smallBlindsTimes = RandomProvider.Next(3, 8);
                    return PlayerAction.Raise(context.SmallBlind * smallBlindsTimes);
                }

                if (playHand == CardValuationType.Playable)
                {
                    var smallBlindsTimes = RandomProvider.Next(8, 15);
                    return PlayerAction.Raise(context.SmallBlind * smallBlindsTimes);
                }

                if (playHand == CardValuationType.Recommended)
                {
                    var smallBlindsTimes = RandomProvider.Next(15, 20);
                    return PlayerAction.Raise(context.SmallBlind * smallBlindsTimes);
                }

                return PlayerAction.CheckOrCall();
            }

            if (context.RoundType == GameRoundType.Flop || context.RoundType == GameRoundType.Turn || context.RoundType == GameRoundType.River)
            {
                var playHand = HandStrengthValuation.PreFlop(this.FirstCard, this.SecondCard);
                if (playHand == CardValuationType.Playable)
                {
                    return PlayerAction.Raise(context.CurrentPot * 1 / 2);
                }

                if (playHand == CardValuationType.Recommended)
                {
                    return PlayerAction.Raise(context.CurrentPot * 3 / 4);
                }
            }

            return PlayerAction.CheckOrCall();
        }
    }
}
