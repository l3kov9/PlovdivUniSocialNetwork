﻿namespace PuSocialNetwork.Tests.Play2048
{
    using App.Areas.Games.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PuSocialNetwork.Services;
    using PuSocialNetwork.Services.Implementations;
    using PuSocialNetwork.Services.Models.Games;

    using static PuSocialNetwork.Common.Enums.Direction;

    [TestClass]
    public class MoveKeyTest
    {
        private IGameService games;
        private GameViewModel game;
        private GameServiceModel serviceGame;

        [TestInitialize]
        public void SetInit()
        {
            this.games = new GameService();
            this.game = new GameViewModel
            {
                Field = new int[,]
                {
                    {4,0,0,0},
                    {2,0,0,0},
                    {8,0,0,0},
                    {16,32,0,0}
                }
            };

            this.serviceGame = new GameServiceModel()
            {
                Field = this.game.Field,
                CurrentScore = this.game.CurrentScore
            };
        }

        [TestMethod]
        public void MoveKeyCommandReturnsFalseIfMatrixNotMoved()
        {
            Assert.AreEqual(false, this.games.MoveKey(this.serviceGame, Left));
            Assert.AreEqual(false, this.games.MoveKey(this.serviceGame, Down));
            Assert.AreEqual(32, this.serviceGame.MaxNumber);
        }

        [TestMethod]
        public void MoveKeyCommandReturnsTrueIfMatrixMoves()
        {
            Assert.AreEqual(true, this.games.MoveKey(this.serviceGame, Right));
            Assert.AreEqual(true, this.games.MoveKey(this.serviceGame, Up));
            Assert.AreEqual(32, this.serviceGame.MaxNumber);
        }
    }
}
