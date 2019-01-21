namespace PuSocialNetwork.App.Areas.Home.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using PuSocialNetwork.App.Infrastructure;
    using Services;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        [Route("/Home/Like")]
        public IActionResult Like(int userId, int postId)
        {
            var success = this.posts.LikeStatus(userId, postId);

            var posts = this.posts.All(1);

            return RedirectToAction(nameof(Index), posts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [Route("/Profile/Logout")]
        public IActionResult Logout()
        {
            this.HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost("UploadFiles")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFiles(UploadFileViewModel fileModel)
        {
            if (!ModelState.IsValid || fileModel.File == null)
            {
                return RedirectToAction(nameof(Profile), new { id = fileModel.UserId });
            }

            using (var memoryStream = new MemoryStream())
            {
                await fileModel.File.CopyToAsync(memoryStream);

                var success = this.users.UpdateImage(fileModel.UserId, memoryStream.ToArray());

                var base64 = Convert.ToBase64String(memoryStream.ToArray());
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                HttpContext.Session.SetString(SessionConstants.SessionUserImage, imgSrc);
            }

            return RedirectToAction(nameof(Profile), new { id = fileModel.UserId });
        }

        private string GetYoutubeCode(string content)
        {
            var query = content.Split('?').Last();
            var result = content.Split(new[] { '=', '&' }, StringSplitOptions.RemoveEmptyEntries)[1];

            return result;
        }
    }
}
