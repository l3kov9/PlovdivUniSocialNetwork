namespace PuSocialNetwork.Services
{
    using Models;
    using System.Collections.Generic;

    public interface IPostService
    {
        bool PostStatus(int userId, string content, bool isYoutube);

        bool LikeStatus(int userId, int postId);

        bool CommentStatus(int userId, int postId, string text);

        IEnumerable<PostListingServiceModel> All(int page = 1, int pageSize = 9);
    }
}
