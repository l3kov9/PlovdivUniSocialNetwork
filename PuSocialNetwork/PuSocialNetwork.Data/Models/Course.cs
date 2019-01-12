namespace PuSocialNetwork.Data.Models
{
    using System.Collections.Generic;

    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
