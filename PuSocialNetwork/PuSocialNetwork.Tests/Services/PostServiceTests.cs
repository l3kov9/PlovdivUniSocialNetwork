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
    public class PostServiceTests
    {
        private const string TestComment = "Test comment";
        private SocialNetworkDbContext db;
        private PostService posts;
        private Random rnd;

        [TestInitialize]
        public void SetInit()
        {
            this.db = GetDatabase();
            this.posts = new PostService(this.db);
            this.rnd = new Random();
        }

        [TestMethod]
        public void AllReturnsLastPageListPosts()
        {
            SeedPosts();
            var result = this.posts.All();

            Assert.AreEqual(9, result.Count());
            Assert.AreEqual(19, result.First().Id);
        }

        [TestMethod]
        public void AllWithPageAndPageSizeReturnsCorrectValues()
        {
            SeedPosts();
            var result = this.posts.All(2, 3);

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(16, result.First().Id);
            Assert.AreEqual(14, result.Last().Id);
        }

        [TestMethod]
        public void CommentStatusSaveCorrectValues()
        {
            SeedPosts();

            var success = this.posts.CommentStatus(1, 19, TestComment);

            Assert.AreEqual(1, this.db.Comments.Count());
            Assert.AreEqual(19, this.db.Comments.First().PostId);
            Assert.AreEqual(TestComment, this.db.Comments.First().Text);
            Assert.AreEqual(true, success);
        }

        [TestMethod]
        public void LikeStatusSaveCorrectValues()
        {
            SeedPosts();

            var success = this.posts.LikeStatus(1, 19);

            Assert.AreEqual(1, this.db.Likes.Count());
            Assert.AreEqual(19, this.db.Likes.First().PostId);
            Assert.AreEqual(1, this.db.Likes.First().UserId);
            Assert.AreEqual(true, success);
        }

        [TestMethod]
        public void PostStatusSavesCorrectValues()
        {
            for (int i = 1; i < 10; i++)
            {
                var success = this.posts.PostStatus(i, TestComment, false);

                Assert.AreEqual(true, success);
            }

            Assert.AreEqual(9, this.db.Posts.Count());
        }

        private void SeedPosts()
        {
            this.db.Add(new User()
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User"
            });

            this.db.SaveChanges();

            for (int i = 1; i < 20; i++)
            {
                this.db.Add(new Post()
                {
                    Id = i,
                    UserId = 1,
                    PostDate = DateTime.UtcNow.AddMinutes(i)
                });
            }

            this.db.SaveChanges();
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
