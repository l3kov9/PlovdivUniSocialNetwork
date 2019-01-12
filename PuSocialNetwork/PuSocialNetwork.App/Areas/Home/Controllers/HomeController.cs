namespace PuSocialNetwork.App.Areas.Home.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        [Area("Home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
