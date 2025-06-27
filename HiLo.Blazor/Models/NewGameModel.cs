using System.ComponentModel.DataAnnotations;

namespace HiLo.Blazor.Models
{
    public class NewGameModel
    {
        [Required]
        public string Player1Name { get; set; } =string.Empty;
        [Required]
        public string Player2Name { get; set; } = string.Empty;
        [Required]
        public int NumberOfRounds { get; set; }
        public int RemoteNumberOfRounds { get; set; }
        public int[] AvailableNumberOfRounds { get; } = { 1, 3, 5, 15 };
        public string GameKey { get; set; } = string.Empty;
        public bool RemotePlayAvailable { get; set; } = false;
        public string[]? OnlinePlayers { get; set; }
    }
}
