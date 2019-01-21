namespace PuSocialNetwork.App.Areas.Blog.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PublishArticleViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
