namespace PuSocialNetwork.Services.Implementations
{
    using Data;
    using Data.Models;
    using Models;
    using Models.Games;
    using System;
    using System.Collections.Generic;
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

        public void AddScoreToPlay2048(int userId, int finalScore, int maxNumber)
        {
            this.db.Play2048Scores.Add(new Play2048Score
            {
                FinalScore = finalScore,
                MaxNumber = maxNumber,
                UserId = userId
            });

            this.db.SaveChanges();
        }

        public IEnumerable<ScoreServiceModel> GetHighScoresToPlay2048(int count)
            => this.db
                .Play2048Scores
                .OrderByDescending(sc => sc.HasWon)
                .ThenByDescending(sc => sc.FinalScore)
                .Select(sc => new ScoreServiceModel
                {
                    Username = sc.User.FirstName + " " + sc.User.LastName,
                    FinalScore = sc.FinalScore,
                    HasWon = sc.HasWon
                })
                .Take(count)
                .ToList();
    }
}

