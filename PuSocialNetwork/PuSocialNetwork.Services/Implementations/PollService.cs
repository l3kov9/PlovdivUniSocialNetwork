namespace PuSocialNetwork.Services.Implementations
{
    using Data;
    using Models.Polls;
    using System.Linq;

    public class PollService : IPollService
    {
        private readonly SocialNetworkDbContext db;

        public PollService(SocialNetworkDbContext db)
        {
            this.db = db;
        }

        public PollServiceModel GetRndPoll(int userId)
            => this.db
                .Polls
                .Select(p => new PollServiceModel()
                {
                    Question = p.Question,
                    Answers = p.Answers.Select(a => new PollAnswerServiceModel()
                    {
                        Id = a.Id,
                        Text = a.Text,
                        TotalClicks = a.TotalClicks
                    })
                    .ToList(),
                    IsVoted = p.Users.Any(u => u.UserId == userId)
                })
                .OrderBy(p=>p.IsVoted)
                .FirstOrDefault();
    }
}
