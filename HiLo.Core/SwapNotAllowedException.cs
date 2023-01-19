namespace HiLo.Core
{
    public class SwapNotAllowedException : Exception
    {
        public SwapNotAllowedException(int boardPosition) : base(string.Format("A card swap at position {0} is not allowed.", boardPosition))
        {

        }
    }
}
