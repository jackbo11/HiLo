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
        public int[] AvailableNumberOfRounds { get; } = new int[] { 1, 3, 5, 15 };
    }
}
