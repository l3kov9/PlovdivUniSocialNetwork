namespace PuSocialNetwork.Services.Models
{
    using System;
    using System.Collections.Generic;

    public class PostListingServiceModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsYoutubeVideo { get; set; }

        public DateTime PostDate { get; set; }

        public string AuthorName { get; set; }

        public int TotalLikes { get; set; }

        public List<CommentServiceModel> Comments { get; set; }
    }
}
