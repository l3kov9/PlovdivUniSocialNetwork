namespace PuSocialNetwork.Tests.Services
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PuSocialNetwork.Services.Implementations;
    using System;
    using System.Linq;

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

        private SocialNetworkDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<SocialNetworkDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new SocialNetworkDbContext(dbOptions);
        }
    }
}
