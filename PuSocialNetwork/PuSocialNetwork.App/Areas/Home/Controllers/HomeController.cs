namespace PuSocialNetwork.App.Areas.Home.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System;
    using System.Linq;

    [Area("Home")]
    public class HomeController : Controller
    {
        private readonly IPostService posts;
        private readonly IUserService users;

        public HomeController(IPostService posts, IUserService users)
        {
            this.posts = posts;
            this.users = users;
        }

        [Route("/Home")]
        public IActionResult Index(int page = 1)
        {
            var posts = this.posts.All(page);

            return View(posts);
        }

        [HttpPost]
        [Route("/Home/Post")]
        public IActionResult Post(PostViewModel post)
        {
            if (post.IsYoutube)
            {
                post.Content = GetYoutubeCode(post.Content);
            }

            var success = this.posts.PostStatus(post.UserId, post.Content, post.IsYoutube);

            var posts = this.posts.All(1);

            return PartialView("_PostPartPartial", posts);
        }

        [HttpPost]
        [Route("/Home/Like")]
        public IActionResult Like(int userId, int postId)
        {
            var success = this.posts.LikeStatus(userId, postId);

            var posts = this.posts.All(1);

            return RedirectToAction(nameof(Index), posts);
        }

        [HttpPost]
        [Route("/Home/Comment")]
        public IActionResult Comment(int userId, int postId, string text)
        {
            var success = this.posts.CommentStatus(userId, postId, text);

            var posts = this.posts.All(1);

            return RedirectToAction(nameof(Index), posts);
        }

        [Route("/Profile/{id}")]
        public IActionResult Profile(int id)
        {
            var user = this.users.GetUserById(id);

            return View(user);
        }

        private string GetYoutubeCode(string content)
        {
            var query = content.Split('?').Last();
            var result = content.Split(new[] { '=', '&' }, StringSplitOptions.RemoveEmptyEntries)[1];

            return result;
        }
    }
}
