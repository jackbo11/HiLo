namespace HiLo.Core
{
    public class Player
    {
        public string Name { get; private set; }
        public int RoundsWon { get; set; }
        public Player() : this("Player")
        {

        }
        public Player(string name) : this(name, 0)
        {

        }
        public Player(string name, int roundsWon)
        {
            Name = name;
            RoundsWon = roundsWon;
        }
    }
}
