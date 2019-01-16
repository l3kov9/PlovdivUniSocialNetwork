namespace PuSocialNetwork.Services
{
    using Models;

    public interface IUserService
    {
        UserServiceModel GetUserByFacNumAndEgn(string facultyNumber, string egn);

        UserServiceModel GetUserById(int id);

        bool UpdateImage(int userId, byte[] image);
    }
}
