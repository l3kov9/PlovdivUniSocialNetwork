namespace PuSocialNetwork.App.Areas.Home.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Area("Home")]
    public class HomeController : Controller
    {
        [Route("/Home")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Profile/{id}")]
        public IActionResult Profile(int id)
        {
            return View();
        }

        [HttpPost]
        [Route("/Home/Post")]
        public IActionResult Post(PostViewModel post)
        {
            var x = 2;
            return View();
        }

        public IActionResult Games()
        {
            return View();
        }
    }
}
