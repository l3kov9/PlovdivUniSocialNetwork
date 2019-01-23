namespace PuSocialNetwork.Tests.Play2048
{
    using App.Areas.Games.Models;
    using PuSocialNetwork.Common.FieldManipulations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PuSocialNetwork.Services;
    using PuSocialNetwork.Services.Implementations;
    using System.Linq;

    [TestClass]
    public class StartGameTest
    {
        private readonly IGameService games;

        public StartGameTest()
        {
            this.games = new GameService();
        }

        [TestMethod]
        public void FieldGetsCorrectValues()
        {
            var game = new GameViewModel();

            for (int i = 0; i < 1000; i++)
            {
                game.Field = games.RestartGameField();
                var nonZeroNumbers = FieldHelper.GetNonZeroNumbers(game.Field);

                Assert.AreEqual(2, nonZeroNumbers.Count);
                Assert.AreEqual(2, nonZeroNumbers.Count(n => n == 2 || n == 4));
                Assert.IsTrue(nonZeroNumbers.Sum() == 4 || nonZeroNumbers.Sum() == 6);
            }
        }
    }
}
