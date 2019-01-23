namespace PuSocialNetwork.Data.Models
{
    public class Play2048Score
    {
        public int Id { get; set; }

        public int FinalScore { get; set; }

        public int MaxNumber { get; set; }

        public bool HasWon => MaxNumber == 2048;

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
