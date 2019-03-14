namespace PuSocialNetwork.Data.Models
{
    public class PollAnswer
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int TotalClicks { get; set; }

        public int PollId { get; set; }

        public Poll Poll { get; set; }
    }
}
