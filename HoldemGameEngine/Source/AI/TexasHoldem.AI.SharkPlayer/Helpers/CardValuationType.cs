namespace TexasHoldem.AI.SharkPlayer.Helpers
{
    public enum CardValuationType
    {
        Unplayable = 0,
        NotRecommended = 1000,
        Risky = 2000,
        Playable = 3000,
        Recommended = 4000,
    }
}
