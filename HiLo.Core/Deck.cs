namespace HiLo.Core
{
    internal class Deck
    {
        private readonly Queue<Card> cardDeck;
        public Deck(Queue<Card> cardDeck)
        {
            this.cardDeck = cardDeck;
        }
        public Card DrawCard()
        {
            if (cardDeck.Count != 0)
            {
                return cardDeck.Dequeue();
            }
            throw new DeckEmptyException();
        }
    }
}
