namespace PuSocialNetwork.App.Areas.Home.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("Home")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Games()
        {
            return View();
        }
    }
}
