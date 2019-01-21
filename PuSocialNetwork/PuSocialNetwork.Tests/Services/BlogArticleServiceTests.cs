namespace PuSocialNetwork.Tests.Services
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PuSocialNetwork.Services.Implementations;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    [TestClass]
    public class BlogArticleServiceTests
    {
        private SocialNetworkDbContext db;
        private BlogArticleService articles;

        [TestInitialize]
        public void SetInit()
        {
            this.db = GetDatabase();
            this.articles = new BlogArticleService(this.db);

        }

        [TestMethod]
        public async Task AllAsyncReturnsCorrectValues()
        {
            await SeedArticles();

            var result = await this.articles.AllAsync(1, 10);

            Assert.AreEqual(20, result.First().Id);
        }

        private async Task SeedArticles()
        {
            for (int i = 1; i <= 20; i++)
            {
                await this.db.AddAsync(new Article()
                {
                    Id = i,
                    Content = $"Content {i}",
                    PublishDate = DateTime.Now.AddHours(-1),
                    Title = $"Title {i}",
                    UserId = i % 5
                });

                await this.db.SaveChangesAsync();
            }
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
