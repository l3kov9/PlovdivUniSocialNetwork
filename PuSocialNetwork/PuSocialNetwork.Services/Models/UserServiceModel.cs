namespace PuSocialNetwork.Services.Models
{
    using System;

    public class UserServiceModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string BirthPlace { get; set; }

        public string Email { get; set; }

        public string FacultyNumber { get; set; }

        public string Course { get; set; }

        public string Egn { get; set; }

        public byte[] ProfileImage { get; set; }
    }
}
