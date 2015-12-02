namespace TexasHoldem.AI.AllInPlayer
{
    using System;
    using TexasHoldem.AI.AllInPlayer.Helpers;
    using TexasHoldem.Logic.Players;

    public class AllInPlayer : BasePlayer
    {
        public override string Name { get; } = "AllInPlayer_" + Guid.NewGuid();

        public override PlayerAction GetTurn(GetTurnContext context)
        {
            var playHand = HandStrengthValuation.PreFlop(this.FirstCard, this.SecondCard);

            if (playHand == CardValuationType.Recommended)
            {
                if (!context.IsAllIn)
                {
                    return PlayerAction.Raise(context.MoneyLeft);
                }
                else
                {
                    return PlayerAction.CheckOrCall();
                }
            }

            return PlayerAction.Fold();
        }
    }
}
