namespace TexasHoldem.AI.SharkPlayer.Helpers
{
    using TexasHoldem.Logic;
    using TexasHoldem.Logic.Extensions;
    using TexasHoldem.Logic.Players;
    using TexasHoldem.AI.SharkPlayer.HandOptions;

    public class PlayerActionFactory
    {
        public static PlayerAction GetPlayerAction(GetTurnContext context, IHandsHolder handsHolder)
        {
            var playHand = handsHolder.PlayHand;
            var bestHand = handsHolder.BestHand;
            var bestHandOnTable = handsHolder.BestHandOnTable;

            if (context.RoundType == GameRoundType.PreFlop && context.MoneyToCall > 0)
            {
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
                if ((int)bestHand < (int)bestHandOnTable && context.MoneyToCall > context.CurrentPot - context.MoneyToCall)
                {
                    return PlayerAction.Fold();
                }

                if ((int)bestHand > (int)HandRankType.Pair)
                {
                    if ((int)bestHand > (int)bestHandOnTable)
                    {
                        return PlayerAction.Raise(context.CurrentPot * 4);
                    }

                    return PlayerAction.Raise(context.CurrentPot * 3);
                }
                else
                {
                    return PlayerAction.CheckOrCall();
                }
            }

            if (context.RoundType == GameRoundType.Turn)
            {
                if ((int)bestHand < (int)bestHandOnTable && context.MoneyToCall > context.CurrentPot - context.MoneyToCall)
                {
                    return PlayerAction.Fold();
                }

                if ((int)bestHand > (int)HandRankType.Pair)
                {
                    if ((int)bestHand > (int)bestHandOnTable)
                    {
                        return PlayerAction.Raise(context.CurrentPot * 4);
                    }

                    return PlayerAction.Raise(context.CurrentPot * 3);
                }
                else
                {
                    return PlayerAction.CheckOrCall();
                }
            }

            if (context.RoundType == GameRoundType.River)
            {
                if ((int)bestHand < (int)bestHandOnTable && context.MoneyToCall > context.CurrentPot - context.MoneyToCall)
                {
                    return PlayerAction.Fold();
                }

                if ((int)bestHand > (int)HandRankType.Pair)
                {
                    if ((int)bestHand > (int)bestHandOnTable)
                    {
                        return PlayerAction.Raise(context.CurrentPot * 6);
                    }

                    return PlayerAction.Raise(context.CurrentPot * 5);
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
