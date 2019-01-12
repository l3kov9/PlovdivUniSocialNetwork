namespace PuSocialNetwork.Services
{
    using Models;

    public interface IUserService
    {
        UserServiceModel GetUserByFacNumAndEgn(string facultyNumber, string egn);
    }
}
