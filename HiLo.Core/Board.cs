namespace HiLo.Core
{
    internal class Board
    {
        private readonly Stack<Card> BoardCards;
        private bool[] swapsUsed = new bool[12];
        public int BoardPosition => BoardCards.Count;
        public Card LastCard => BoardCards.Peek();
        private bool CardSwapAllowedPositions => BoardPosition == 4 || BoardPosition == 8;
        public bool CardSwapAllowed => CardSwapAllowedPositions && !swapsUsed[BoardPosition];
        public Board()
        {
            BoardCards = new Stack<Card>();
        }
        public void Add(Card card)
        {
            BoardCards.Push(card);
        }
        public void Swap(Card card)
        {
            if (CardSwapAllowed)
            {
                BoardCards.Pop();
                Add(card);
                swapsUsed[BoardPosition] = true;
            }
            else
            {
                throw new SwapNotAllowedException(BoardPosition);
            }
        }
    }
}
