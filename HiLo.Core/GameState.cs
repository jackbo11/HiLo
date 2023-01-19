namespace HiLo.Core
{
    public class GameState
    {
        public Action ActionPerformed { get; set; }
        public Card? NewCard { get; set; }
        public Player? CurrentPlayer { get; set; }
        public bool HasPlayerSwapped { get; set; }
        public bool IsGameWon { get; set; }
        public bool CanSwapCard { get; set; }
        public int RoundPosition { get; set; }
    }
}
