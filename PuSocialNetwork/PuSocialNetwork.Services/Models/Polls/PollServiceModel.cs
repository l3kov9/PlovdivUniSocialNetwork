namespace PuSocialNetwork.Services.Models.Polls
{
    using System.Collections.Generic;

    public class PollServiceModel
    {
        public string Question { get; set; }

        public List<PollAnswerServiceModel> Answers { get; set; }

        public bool IsVoted { get; set; }
    }
}
