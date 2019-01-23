namespace PuSocialNetwork.Services
{
    using Models.Games;
    using System;

    public interface IGameService
    {
        int[,] RestartGameField();

        bool MoveKey(GameServiceModel game, Enum direction);
    }
}
