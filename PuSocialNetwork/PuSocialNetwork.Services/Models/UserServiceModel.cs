namespace PuSocialNetwork.Services.Models
{
    public class UserServiceModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string FacultyNumber { get; set; }

        public string Egn { get; set; }

        public byte[] ProfileImage { get; set; }
    }
}
