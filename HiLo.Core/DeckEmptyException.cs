namespace HiLo.Core
{
    internal class DeckEmptyException : Exception
    {
        public DeckEmptyException() : base("An attempt was made to draw a card from a deck while the deck was empty.")
        {

        }
    }
}
