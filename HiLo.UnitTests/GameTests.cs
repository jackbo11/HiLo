using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiLo.UnitTests
{
    internal class GameTests
    {
        [TestCase(1, false)]
        [TestCase(3, true)]
        [TestCase(4, false)]
        [TestCase(7, true)]
        [TestCase(8, false)]
        public void CanSwapInCorrectPlaces(int loopCount, bool canSwap)
        {
            var game = new Game(new Player("Bob"), new Player("Dave"), 1);
            game.StartRound();
            GameState? gs = new();
            for (int i = 0; i < loopCount; i++)
            {
                gs = game.Higher();
            }
            Assert.That(gs?.CanSwapCard, Is.EqualTo(canSwap));
        }
    }
}
