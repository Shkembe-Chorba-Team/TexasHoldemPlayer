namespace TexasHoldem.AI.SharkPlayer.Helpers
{
    using System.Linq;

    using TexasHoldem.Logic;
    using System.Collections.Generic;
    using TexasHoldem.Logic.Cards;
    using TexasHoldem.Logic.Helpers;
    using System;

    public class TableStrengthOpportunities
    {
        // Not precise.
        public static HandRankType GetBestPossibleHand(IReadOnlyCollection<Card> communityCards, params Card[] playerCards)
        {
            var allCards = new List<Card>(communityCards);

            if (allCards.GroupBy(c => c.Type).Any(gr => gr.Count() >= 2))
            {
                if (CheckIfPlayerHasOneCard(allCards, playerCards))
                {
                    return HandRankType.ThreeOfAKind;
                }

                return HandRankType.FourOfAKind;
            }

            if (allCards.GroupBy(c => c.Type).Any(gr => gr.Count() > 1))
            {
                return HandRankType.FullHouse;
            }

            var canHaveStraight = CheckIfPossibleStraight(allCards);
            var canHaveFlush = allCards.All(c => c.Suit != allCards[0].Suit);
            if (canHaveFlush && canHaveStraight && CheckIfPlayerCardsContainFromSequence(allCards, playerCards))
            {
                return HandRankType.StraightFlush;
            }
            else if (canHaveFlush)
            {
                return HandRankType.Flush;
            }

            if (canHaveStraight)
            {
                return HandRankType.Straight;
            }

            return HandRankType.TwoPairs;
        }

        private static bool CheckIfPlayerHasOneCard(List<Card> allCards, Card[] playerCards, int groupOf = 2)
        {
            var allTypes = allCards.GroupBy(c => c.Type).Where(gr => gr.Count() >= groupOf);

            return allTypes.Any(gr => gr.Any(c => c.Type == playerCards[0].Type || c.Type == playerCards[1].Type));
        }

        private static bool CheckIfPlayerCardsContainFromSequence(List<Card> allCards, Card[] playerCards)
        {

            var allTypes = allCards.Select(x => (int)x.Type).ToArray();

            if (playerCards.Length < 2)
            {
                return false;
            }

            if (allTypes.Any(t => t == (int)playerCards[0].Type || t == (int)playerCards[1].Type))
            {
                return true;
            }

            return false;
        }

        private static bool CheckIfPossibleStraight(List<Card> allCards)
        {
            var allTypes = allCards.Select(x => (int)x.Type).ToArray();

            allTypes = allTypes.OrderBy(a => a).ToArray();

            if (Math.Abs(allTypes[0] - allTypes[2]) >= 5)
            {
                return false;
            }

            return true;
        }
    }
}
