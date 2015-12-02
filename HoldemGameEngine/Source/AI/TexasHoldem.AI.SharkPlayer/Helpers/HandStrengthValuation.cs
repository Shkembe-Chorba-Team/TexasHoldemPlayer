namespace TexasHoldem.AI.SharkPlayer.Helpers
{
    using System;
    using TexasHoldem.Logic.Cards;

    public static class HandStrengthValuation
    {
        // http://www.thepokerbank.com/strategy/basic/starting-hand-selection/chen-formula/
        public static CardValuationType PreFlop(Card firstCard, Card secondCard)
        {
            double totalHandScore = 0;
            int higherCard = Math.Max((int) firstCard.Type, (int) secondCard.Type);

            switch (higherCard)
            {
                case (int)CardType.Ace:
                    totalHandScore = 10;
                    break;
                case (int)CardType.King:
                    totalHandScore = 8;
                    break;
                case (int)CardType.Queen:
                    totalHandScore = 7;
                    break;
                case (int)CardType.Jack:
                    totalHandScore = 6;
                    break;
                default:
                    totalHandScore = (double)higherCard / 2;
                    break;
            }

            if (firstCard.Type == secondCard.Type)
            {
                if (totalHandScore > 5)
                {
                    totalHandScore *= 2;
                }
                else
                {
                    totalHandScore = 5;
                }
            }

            if (firstCard.Suit == secondCard.Suit)
            {
                totalHandScore += 2;
            }

            var cardGap = Math.Abs((int)firstCard.Type - (int)secondCard.Type);

            switch (cardGap)
            {
                case 0:
                case 1:
                    break;
                case 2:
                    totalHandScore -= 1;
                    break;
                case 3:
                    totalHandScore -= 2;
                    break;
                case 4:
                    totalHandScore -= 4;
                    break;
                default:
                    totalHandScore -= 5;
                    break;
            }

            if (cardGap == 1 || cardGap == 2 && higherCard < (int)CardType.Queen)
            {
                totalHandScore += 1;
            }

            if (totalHandScore > 7)
            {
                return CardValuationType.Recommended;
            }

            if (totalHandScore > 6)
            {
                return CardValuationType.Playable;
            }

            if (totalHandScore > 5)
            {
                return CardValuationType.Risky;
            }

            if (totalHandScore > 4)
            {
                return CardValuationType.NotRecommended;
            }

            return CardValuationType.Unplayable;
        }
    }
}
