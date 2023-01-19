using HiLo.Core;

namespace HiLo.Cli
{
    class Program
    {
        private static ManualResetEvent waitHandle = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            Console.Write("Player 1 Name: ");
            string player1Name = Console.ReadLine();
            Console.Write("Player 2 Name: ");
            string player2Name = Console.ReadLine();
            Console.Write("Number of rounds: ");
            _ = int.TryParse(Console.ReadLine(), out int noOfRounds);
            Game game = new Game(new Player(player1Name), new Player(player2Name), noOfRounds);
            game.GameStateChanged += Game_GameStateChanged;
            while (!game.GameOver)
            {
                game.StartRound();
                waitHandle.WaitOne();
                Ask(game);
                while (!game.RoundOver)
                {
                    Ask(game);
                }
                Console.WriteLine($"{game.Player1.Name} - Rounds Won: {game.Player1.RoundsWon}");
                Console.WriteLine($"{game.Player2.Name} - Rounds Won: {game.Player2.RoundsWon}");
            }

        }
        public static void Ask(Game game)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "h":
                    game.Higher();
                    break;
                case "l":
                    game.Lower();
                    break;
                case "s":
                    try
                    {
                        game.Swap();
                    }
                    catch (SwapNotAllowedException sna)
                    {
                        Console.WriteLine(sna.Message);
                    }
                    break;
                default:
                    game.Higher();
                    break;
            }
            waitHandle.WaitOne();
        }
        private static void Game_GameStateChanged(object sender, GameStateChangedEventArgs e)
        {


            if (e.HasPlayerSwapped)
            {
                Console.WriteLine("Swap player!");
            }
            Console.WriteLine($"{e.CurrentPlayer.Name}, the card is the {e.NewCard}");
            if (e.IsGameWon)
            {
                Console.WriteLine($"Round Over! {e.CurrentPlayer.Name} won!");
            }
            else
            {
                Console.WriteLine(e.CanSwapCard ? "Higher, lower or swap? " : "Higher or lower? ");
            }

            waitHandle.Set();
        }
    }
}
