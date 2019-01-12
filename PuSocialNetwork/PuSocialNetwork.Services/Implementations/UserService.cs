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

        public int GetUserIdByFacNumAndEgn(string facultyNumber, string egn)
            => this.db
                .Users
                .Where(u => u.FacultyNumber == facultyNumber && u.Egn == egn)
                .Select(u => u.Id)
                .FirstOrDefault();
    }
}
