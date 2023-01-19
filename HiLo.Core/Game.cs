namespace HiLo.Core
{
    public class Game
    {
        private Round? currentRound;
        private readonly Random random;
        public int NumberOfRounds { get; private set; }
        public int RoundCount { get; private set; }
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public Player? WinningPlayer { get
            {
                return GameOver ? (Player1.RoundsWon > Player2.RoundsWon ? Player1 : Player2) : null;
            }
        }
        public bool GameOver => RoundCount >= NumberOfRounds;
        public bool RoundOver { get; private set; }
        private Player? CurrentPlayer => currentRound?.CurrentPlayer ?? null;
 
        public Game(Player player1, Player player2, int numberOfRounds)
        {
            random = new Random();
            Player1 = player1;
            Player2 = player2;
            NumberOfRounds = numberOfRounds;
            RoundCount = 0;
        }

        public GameState? StartRound()
        {
            if (!GameOver)
            {
                RoundCount++;
                currentRound = new Round(Player1, Player2, DetermineStartingPlayer());
                return currentRound.Start();
            }
            return null;
        }

        private int DetermineStartingPlayer()
        {
            if (CurrentPlayer == Player1)
            {
                return 2;
            }
            else if (CurrentPlayer == Player2)
            {
                return 1;
            }
            else
            {
                return random.Next(1, 3);
            }
        }

        public GameState? Higher()
        {
            return currentRound?.Higher();
        }

        public GameState? Lower()
        {
            return currentRound?.Lower();
        }

        public GameState? Swap()
        {
            return currentRound?.Swap();
        }
    }
}
