using HiLo.Core;

namespace HiLo.Blazor.Models
{
    public class GameStatusModel
    {
        public int CurrentRound { get; set; }
        public int TotalRounds { get; set; }
        public Player CurrentPlayer { get; set; }
        public Player[] Players { get; set; } 
    }
}
