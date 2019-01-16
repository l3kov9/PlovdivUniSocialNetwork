namespace PuSocialNetwork.Services.Implementations
{
    using Data;
    using Models;
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
                    LastName = u.LastName
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
                    ProfileImage = u.ProfileImage
                })
                .FirstOrDefault();
    }
}
