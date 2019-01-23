namespace PuSocialNetwork.Tests.Services
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PuSocialNetwork.Services.Implementations;
    using System;
    using System.Linq;
    using System.Text;

    [TestClass]
    public class UserServiceTests
    {
        private const string TestUserNumber = "5555555555";
        private SocialNetworkDbContext db;
        private UserService users;

        [TestInitialize]
        public void Init()
        {
            this.db = GetDatabase();
            this.users = new UserService(this.db);
        }

        [TestMethod]
        public void GetUserByFacNumAndEgnReturnsCorrectUser()
        {
            SeedUsers();

            var result = this.users.GetUserByFacNumAndEgn(TestUserNumber, TestUserNumber);

            Assert.AreEqual(5, result.Id);
        }

        [TestMethod]
        public void GetUserByFacNumAndEgnReturnsNullIfInvalidInputs()
        {
            var result = this.users.GetUserByFacNumAndEgn(TestUserNumber, TestUserNumber);

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GetHighScoresReturnsEmptyListIfNoScoresInDb()
        {
            var result = this.users.GetHighScoresToPlay2048(10);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetHighScoresReturnsCorrectResultWithCorrectValues()
        {
            const int count = 10;

            SeedUsersAndScores();
            var result = this.users.GetHighScoresToPlay2048(count);

            Assert.AreEqual(count, result.Count());
            Assert.IsTrue(result.ElementAt(0).HasWon == result.ElementAt(2).HasWon
                && result.ElementAt(2).HasWon != result.ElementAt(3).HasWon);
        }

        [TestMethod]
        public void AddScoreShouldSaveCorrectDataWithValidValues()
        {
            const int userId = 1;
            const int finalScore = 12264;
            const int maxNumber = 2048;

            SeedUsersAndScores();

            this.users.AddScoreToPlay2048(userId, finalScore, maxNumber);
            var savedEntry = this.db.Play2048Scores.Last();

            Assert.AreEqual(userId, savedEntry.User.Id);
            Assert.AreEqual(finalScore, savedEntry.FinalScore);
            Assert.AreEqual(maxNumber, savedEntry.MaxNumber);
        }


        private void SeedUsers()
        {
            for (int i = 1; i <= 10; i++)
            {
                var userNumber = GetUserNumber(i);

                this.db.Users.Add(new User
                {
                    Id = i,
                    Egn = userNumber.ToString(),
                    FacultyNumber = userNumber.ToString()
                });

                this.db.SaveChanges();
            }
        }

        private void SeedUsersAndScores()
        {
            for (int i = 1; i <= 5; i++)
            {
                var user = new User()
                {
                    Id = i
                };

                this.db.Add(user);
            }

            this.db.SaveChanges();

            for (int i = 0; i < 20; i++)
            {
                var score = new Play2048Score()
                {
                    FinalScore = i * 100,
                    MaxNumber = i % 5 == 0 ? 2048 : i * 20,
                    UserId = i % 6
                };

                this.db.Add(score);
            }

            this.db.SaveChanges();
        }

        private static StringBuilder GetUserNumber(int i)
        {
            var userNumber = new StringBuilder();
            for (int j = 0; j < 10; j++)
            {
                userNumber.Append(i);
            }

            return userNumber;
        }

        private SocialNetworkDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<SocialNetworkDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new SocialNetworkDbContext(dbOptions);
        }
    }
}
