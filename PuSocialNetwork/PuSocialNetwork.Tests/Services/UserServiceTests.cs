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
