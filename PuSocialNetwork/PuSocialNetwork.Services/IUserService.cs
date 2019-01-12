namespace PuSocialNetwork.Services
{
    using Models;

    public interface IUserService
    {
        int GetUserIdByFacNumAndEgn(string facultyNumber, string egn);
    }
}
