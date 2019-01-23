namespace PuSocialNetwork.Services
{
    using Models;
    using Models.Games;
    using System.Collections.Generic;

    public interface IUserService
    {
        UserServiceModel GetUserByFacNumAndEgn(string facultyNumber, string egn);

        UserServiceModel GetUserById(int id);

        bool UpdateImage(int userId, byte[] image);

        void AddScoreToPlay2048(int userId, int finalScore, int maxNumber);

        IEnumerable<ScoreServiceModel> GetHighScoresToPlay2048(int count);
    }
}
