using HiLo.Core;
using System.Text;

namespace HiLo.Blazor.Helpers
{
    public static class CardExtensions
    {
        public static string GetImageName(this Card card)
        {
            var sb = new StringBuilder();
            sb.Append(card.Suit.ToString().Substring(0, 2).ToLower());
            sb.Append(card.Rank >= Rank.Two && card.Rank <= Rank.Ten ? ((int)card.Rank).ToString() : card.Rank.ToString().Substring(0, 1).ToLower());
            sb.Append(".svg");
            return sb.ToString();
        }
    }
}
