namespace PuSocialNetwork.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System;
    using System.Diagnostics;
    using System.Net;

    public class HomeController : Controller
    {
        private const string QuotesUrl = @"http://nvp-playground.azurewebsites.net/api/quotes/random/txt";

        private readonly IUserService users;

        public HomeController(IUserService users)
        {
            this.users = users;
        }

        public IActionResult Index()
        {
            return View(nameof(Index), GetQuote());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), GetQuote());
            }

            var result = this.users.GetUserByFacNumAndEgn(user.FacultyNumber, user.Egn);

            if(result == null)
            {
                ModelState.AddModelError(string.Empty, "Грешно потребителско име или парола");

                return View(nameof(Index), GetQuote());
            }

            return RedirectToAction("Index", "Home", new { area = "Home", model = result });
        }

        private static QuoteViewModel GetQuote()
        {
            var client = new WebClient();
            var quoteAsText = client.DownloadString(QuotesUrl);

            var quoteLines = quoteAsText.Split(Environment.NewLine);

            return new QuoteViewModel
            {
                Text = quoteLines[0],
                Author = quoteLines[1]
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
