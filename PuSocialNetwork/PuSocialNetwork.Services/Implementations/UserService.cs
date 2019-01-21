namespace PuSocialNetwork.Services.Implementations
{
    using Data;
    using Models;
    using System;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly SocialNetworkDbContext db;

        public UserService(SocialNetworkDbContext db)
        {
            this.db = db;
        }

        public UserServiceModel GetUserByFacNumAndEgn(string facultyNumber, string egn)
            => this.db
                .Users
                .Where(u => u.FacultyNumber == facultyNumber && u.Egn == egn)
                .Select(u => new UserServiceModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    ProfileImage = u.ProfileImage
                })
                .FirstOrDefault();

        public UserServiceModel GetUserById(int id)
            => this.db
                .Users
                .Where(u => u.Id == id)
                .Select(u => new UserServiceModel
                {
                    Id = id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    BirthDate = u.BirthDate,
                    BirthPlace = u.BirthPlace,
                    Email = u.Email,
                    FacultyNumber = u.FacultyNumber,
                    Egn = u.Egn,
                    Course = u.Course.Name,
                    ProfileImage = u.ProfileImage,
                    Posts = u.Posts.OrderByDescending(p => p.PostDate)
                        .Select(p => new PostListingServiceModel
                        {
                            Id = p.Id,
                            Content = p.Content,
                            PostDate = p.PostDate,
                            IsYoutubeVideo = p.IsYoutubeVideo,
                            TotalLikes = p.Likes.Count,
                            Image = p.User.ProfileImage,
                            Comments = p.Comments.Select(c => new CommentServiceModel
                            {
                                Text = c.Text,
                                AuthorName = c.User.FirstName + " " + c.User.LastName
                            })
                            .ToList()
                        })
                        .ToList()
                })
                .FirstOrDefault();

        public bool UpdateImage(int userId, byte[] image)
        {
            try
            {
                this.db
                    .Users
                    .Where(u => u.Id == userId)
                    .FirstOrDefault()
                    .ProfileImage = image;

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
