namespace HiLo.Core
{
    internal class Round
    {
        private readonly Deck masterDeck;
        private readonly Board board;
        private readonly Player[] players = new Player[2];
        private int currentPlayerNumber;
        public Player CurrentPlayer => players[currentPlayerNumber];
        public bool CardSwapAllowed => board.CardSwapAllowed;
        public int GamePosition => board.BoardPosition;
        public Round(Player player1, Player player2, int startingPlayer)
        {
            ShuffledDeckFactory sdf = new ShuffledDeckFactory();
            masterDeck = sdf.GetDeck();
            board = new Board();
            players[0] = player1;
            players[1] = player2;
            currentPlayerNumber = startingPlayer - 1;
        }
        public GameState Start()
        {
            DrawCard();
            return new GameState { ActionPerformed = Action.Start, CurrentPlayer = CurrentPlayer, HasPlayerSwapped = false, IsGameWon = false, NewCard = board.LastCard, CanSwapCard = board.CardSwapAllowed, RoundPosition = GamePosition };
        }
        public GameState Higher()
        {
            return HandleHigherOrLower(1);
        }
        public GameState Lower()
        {
            return HandleHigherOrLower(-1);
        }
        public GameState Swap()
        {
            if (CardSwapAllowed)
            {
                Card newCard = masterDeck.DrawCard();
                board.Swap(newCard);
                return new GameState { ActionPerformed = Action.Swap, CurrentPlayer = CurrentPlayer, IsGameWon = false, NewCard = newCard, HasPlayerSwapped = false, CanSwapCard = board.CardSwapAllowed, RoundPosition = GamePosition };
            }
            else
            {
                throw new SwapNotAllowedException(board.BoardPosition);
            }
        }
        private GameState HandleHigherOrLower(int targetMoveValue)
        {
            GameState gs = new() { ActionPerformed = targetMoveValue == 1 ? Action.Higher : Action.Lower };
            if (Move() == targetMoveValue)
            {
                gs.HasPlayerSwapped = false;
            }
            else
            {
                SwapPlayer();
                gs.HasPlayerSwapped = true;
            }
            gs.NewCard = board.LastCard;
            gs.CurrentPlayer = CurrentPlayer;
            gs.IsGameWon = IsGameWon();
            gs.CanSwapCard = board.CardSwapAllowed;
            gs.RoundPosition = GamePosition;
            return gs;
        }
        private int Move()
        {
            Card nextCard = masterDeck.DrawCard();
            int comparisonResult = nextCard.CompareTo(board.LastCard);
            board.Add(nextCard);
            return comparisonResult;

        }
        private void DrawCard()
        {
            board.Add(masterDeck.DrawCard());
        }
        private bool IsGameWon()
        {
            if (GamePosition >= 12)
            {
                CurrentPlayer.RoundsWon++;
                return true;
            }
            return false;
        }
        private void SwapPlayer()
        {
            currentPlayerNumber = currentPlayerNumber == 1 ? 0 : 1;
        }
    }
}
