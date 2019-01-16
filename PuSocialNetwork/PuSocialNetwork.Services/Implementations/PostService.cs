namespace PuSocialNetwork.Services.Implementations
{
    using Data;
    using Data.Models;
    using PuSocialNetwork.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PostService : IPostService
    {
        private readonly SocialNetworkDbContext db;

        public PostService(SocialNetworkDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PostListingServiceModel> All(int page = 1, int pageSize = 9)
            => this.db
                .Posts
                .OrderByDescending(p => p.PostDate)
                .Select(p => new PostListingServiceModel()
                {
                    Id = p.Id,
                    Content = p.Content,
                    IsYoutubeVideo = p.IsYoutubeVideo,
                    PostDate = p.PostDate,
                    AuthorName = p.User.FirstName + " " + p.User.LastName,
                    TotalLikes = p.Likes.Count,
                    Comments = p.Comments.Select(c => new CommentServiceModel()
                    {
                        Text = c.Text,
                        AuthorName = c.User.FirstName + " " + c.User.LastName
                    })
                    .ToList()
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

        public bool CommentStatus(int userId, int postId, string text)
        {
            try
            {
                this.db
                    .Comments
                    .Add(new Comment()
                    {
                        Text = text,
                        UserId = userId,
                        PostId = postId
                    });

                this.db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool LikeStatus(int userId, int postId)
        {
            try
            {
                this.db
                    .Likes
                    .Add(new Like()
                    {
                        UserId = userId,
                        PostId = postId
                    });

                this.db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool PostStatus(int userId, string content, bool isYoutube)
        {
            try
            {
                this.db
                .Posts
                .Add(new Post()
                {
                    UserId = userId,
                    Content = content,
                    IsYoutubeVideo = isYoutube,
                    PostDate = DateTime.UtcNow
                });

                this.db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
