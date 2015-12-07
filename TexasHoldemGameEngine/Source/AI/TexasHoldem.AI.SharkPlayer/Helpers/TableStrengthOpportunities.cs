namespace TexasHoldem.AI.SharkPlayer.Helpers
{
    using System.Linq;

    using TexasHoldem.Logic;
    using System.Collections.Generic;
    using TexasHoldem.Logic.Cards;
    using System;

    public class TableStrengthOpportunities
    {
        /// <summary>
        /// Returns the best possible hand a player can have.
        /// </summary>
        /// <param name="communityCards">All cards on the table.</param>
        /// <param name="playerCards">All cards in yourhand.</param>
        /// <returns>A variable of type HandRankType with the best possible hand.</returns>
        public static HandRankType GetBestPossibleHand(IReadOnlyCollection<Card> communityCards, params Card[] playerCards)
        {
            // Not precise.
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

        /// <summary>
        /// Checks if player has one card.
        /// </summary>
        /// <param name="allCards">A list of all cards on the table.</param>
        /// <param name="playerCards">An Array with the player's cards.</param>
        /// <param name="groupOf">A group of cards as an int.</param>
        /// <returns>A boolean that says if player has one card.</returns>
        private static bool CheckIfPlayerHasOneCard(List<Card> allCards, Card[] playerCards, int groupOf = 2)
        {
            var allTypes = allCards.GroupBy(c => c.Type).Where(gr => gr.Count() >= groupOf);

            return allTypes.Any(gr => gr.Any(c => c.Type == playerCards[0].Type || c.Type == playerCards[1].Type));
        }

        /// <summary>
        /// Checks if player's hands form a sequence.
        /// </summary>
        /// <param name="allCards">A list of all cards on the table.</param>
        /// <param name="playerCards">An array from the player's cards.</param>
        /// <returns>A boolean that sais if there is a sequence.</returns>
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

        /// <summary>
        /// Checks if player can possibly have a straight.
        /// </summary>
        /// <param name="allCards">List of all cards.</param>
        /// <returns>A boolean that sais if there is any chance of having a straight.</returns>
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
