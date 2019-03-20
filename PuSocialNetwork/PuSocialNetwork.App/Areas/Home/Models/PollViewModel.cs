namespace PuSocialNetwork.App.Areas.Home.Models
{
    using Services.Models;
    using Services.Models.Polls;
    using System.Collections.Generic;

    public class PollViewModel
    {
        public IEnumerable<PostListingServiceModel> Posts { get; set; }

        public PollServiceModel Poll { get; set; }
    }
}
