using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bunchkins.Domain.Core;

namespace Bunchkin.Test
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void DrawCardTest()
        {
            var game = new Game();

            var card = game.DrawDoorCard();

            Assert.IsNotNull(card);
        }
    }
}
