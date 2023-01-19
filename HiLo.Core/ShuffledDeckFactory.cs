using System.Security.Cryptography;

namespace HiLo.Core
{
    internal class ShuffledDeckFactory : DeckFactory
    {
        private List<Card> ShuffledDeck = new List<Card>();
        public override Deck GetDeck()
        {

            NewDeck();
            ShuffleDeck();
            return new Deck(new Queue<Card>(ShuffledDeck));
        }
        private void NewDeck()
        {

            foreach (Rank r in Enum.GetValues(typeof(Rank)))
            {
                foreach (Suit s in Enum.GetValues(typeof(Suit)))
                {
                    ShuffledDeck.Add(new Card(r, s));
                }
            }
        }
        private void ShuffleDeck()
        {
            using (var provider = RandomNumberGenerator.Create())
            {
                int n = ShuffledDeck.Count;
                while (n > 1)
                {
                    byte[] box = new byte[1];
                    do provider.GetBytes(box);
                    while (!(box[0] < n * (byte.MaxValue / n)));
                    int k = (box[0] % n);
                    n--;
                    (ShuffledDeck[n], ShuffledDeck[k]) = (ShuffledDeck[k], ShuffledDeck[n]);
                }
            }
        }
    }
}
