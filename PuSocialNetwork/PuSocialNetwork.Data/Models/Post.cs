namespace PuSocialNetwork.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Post
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsYoutubeVideo { get; set; }

        public DateTime PostDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<Like> Likes { get; set; } = new List<Like>();
    }
}
