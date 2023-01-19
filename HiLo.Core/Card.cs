namespace HiLo.Core
{
    public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }
    public enum Suit { Clubs = 1, Hearts, Diamonds, Spades }
    public class Card : IComparable<Card>, IEquatable<Card>
    {
        public Rank Rank { get; private set; }
        public Suit Suit { get; private set; }

        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }
        public override string ToString()
        {
            if (Rank != 0)
            {
                return string.Format("{0} of {1}", Rank, Suit);
            }
            return "Undefined card";

        }

        public int CompareTo(Card? other)
        {
            if (other == null) { return 1; }
            return Rank.CompareTo(other.Rank);
        }

        public bool Equals(Card? other)
        {
            if (other == null) { return false; }
            return Rank.Equals(other?.Rank);
        }
    }
}
