namespace PuSocialNetwork.App.Areas.Games.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using Services.Models.Games;
    using System.Linq;
    using Infrastructure.Extensions;

    using static Common.FieldManipulations.FieldHelper;
    using static Common.Enums.Direction;
    using System;
    using PuSocialNetwork.App.Infrastructure;

    [Area("Games")]
    public class GamesController : Controller
    {
        private const string SessionGameFieldKey = "GameFiveld";
        private const string SessionScoreKey = "CurrentScore";
        private const string SessionMaxNumKey = "MaxNumber";
        private const string SessionSendResultKey = "SendResult";
        private const int TopPlayersViewCount = 10;

        private readonly IUserService userManager;
        private readonly IGameService gameManager;
        private readonly GameViewModel game;

        public GamesController(IUserService userManager, IGameService gameManager)
        {
            this.userManager = userManager;
            this.gameManager = gameManager;
            this.game = new GameViewModel();
        }

        [Route("/Games/All")]
        public IActionResult All()
        {
            return View();
        }

        [Route("/Games/Play2048")]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(this.GetSessionString(SessionGameFieldKey)))
            {
                RestartField();
            }
            else
            {
                this.game.Field = GetMatrixFromString(this.GetSessionString(SessionGameFieldKey));
                this.game.CurrentScore = this.GetSessionInt(SessionScoreKey);
                this.game.MaxNumber = this.GetSessionInt(SessionMaxNumKey);
                this.game.IsFinished = Convert.ToBoolean(this.GetSessionInt(SessionSendResultKey));
            }

            this.game.HighScores = this.userManager.GetHighScoresToPlay2048(TopPlayersViewCount);

            return View(this.game);
        }

        [HttpPost]
        [Route("/Games/{direction}")]
        public IActionResult Index(string direction)
        {
            if (string.IsNullOrEmpty(direction))
            {
                RestartField();
            }
            else
            {
                var gameField = GetMatrixFromString(this.GetSessionString(SessionGameFieldKey));
                var currentScore = this.GetSessionInt(SessionScoreKey);
                var directionEnum = ReadDirection(direction);
                var gameServiceModel = MoveGameField(gameField, currentScore, directionEnum);

                this.game.Field = gameServiceModel.Field;
                this.game.CurrentScore = gameServiceModel.CurrentScore;
                this.game.IsFinished = gameServiceModel.IsFinished;
                this.game.MaxNumber = gameServiceModel.MaxNumber;

                this.SetSessionString(SessionGameFieldKey, ConvertMatrixToString(this.game.Field));
                this.SetSessionInt(SessionScoreKey, this.game.CurrentScore);
                this.SetSessionInt(SessionMaxNumKey, this.game.MaxNumber);
            }

            return PartialView("_GameBoardPartial", this.game);
        }

        [HttpPost]
        [Route("/Games/SaveGame")]
        public IActionResult SaveGame()
        {
            if (this.GetSessionInt(SessionSendResultKey) == 1)
            {
                return PartialView("_HighScoresPartial", this.game.HighScores);
            }
            var finalScore = this.GetSessionInt(SessionScoreKey);
            var maxNumber = this.GetSessionInt(SessionMaxNumKey);

            this.userManager.AddScoreToPlay2048(this.GetSessionInt(SessionConstants.SessionUserId), finalScore, maxNumber);
            this.game.HighScores = this.userManager.GetHighScoresToPlay2048(TopPlayersViewCount);
            this.SetSessionInt(SessionSendResultKey, 1);

            return PartialView("_HighScoresPartial", this.game.HighScores);
        }

        [HttpPost]
        [Route("/Games/NewGame")]
        public IActionResult NewGame()
        {
            RestartField();

            return PartialView("_GameBoardPartial", this.game);
        }

        private Enum ReadDirection(string direction)
        {
            var result = Left;

            switch (direction)
            {
                case "left":
                    result = Left;
                    break;
                case "right":
                    result = Right;
                    break;
                case "up":
                    result = Up;
                    break;
                case "down":
                    result = Down;
                    break;
                default:
                    break;
            }

            return result;
        }

        private void RestartField()
        {
            this.game.Field = this.gameManager.RestartGameField();

            this.SetSessionString(SessionGameFieldKey, ConvertMatrixToString(this.game.Field));
            this.SetSessionInt(SessionScoreKey, 0);
            this.SetSessionInt(SessionMaxNumKey, this.game.MaxNumber);
            this.SetSessionInt(SessionSendResultKey, 0);
        }

        private GameServiceModel MoveGameField(int[,] gameField, int currentScore, Enum direction)
        {
            var gameServiceModel = new GameServiceModel()
            {
                Field = gameField,
                CurrentScore = currentScore
            };

            var isMoved = this.gameManager.MoveKey(gameServiceModel, direction);

            if (isMoved)
            {
                AddRandomNumber(gameServiceModel.Field);
            }
            else
            {
                var hasZeroValues = HasZeroValues(gameServiceModel.Field);

                if (!hasZeroValues)
                {
                    var isOver = IsGameOver(gameServiceModel.Field);
                    gameServiceModel.IsFinished = isOver;
                }
            }

            return gameServiceModel;
        }

        private bool IsGameOver(int[,] field)
        {
            for (int i = 0; i < field.GetLength(0) - 1; i++)
            {
                for (int k = 0; k < field.GetLength(1) - 1; k++)
                {
                    if (field[i, k] == field[i, k + 1] || field[i, k] == field[i + 1, k])
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < field.GetLength(0) - 1; i++)
            {
                if (field[field.GetLength(0) - 1, i] == field[field.GetLength(0) - 1, i + 1]
                    || field[i, field.GetLength(0) - 1] == field[i + 1, field.GetLength(0) - 1])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
