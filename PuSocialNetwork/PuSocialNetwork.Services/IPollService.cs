namespace PuSocialNetwork.Services
{
    using Models.Polls;

    public interface IPollService
    {
        PollServiceModel GetRndPoll(int userId);
    }
}
