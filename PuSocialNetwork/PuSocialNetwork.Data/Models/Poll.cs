namespace PuSocialNetwork.Data.Models
{
    using System.Collections.Generic;

    public class Poll
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public List<PollAnswer> Answers { get; set; }

        public List<UserPoll> Users { get; set; } = new List<UserPoll>();
    }
}
