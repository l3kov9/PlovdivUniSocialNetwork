namespace PuSocialNetwork.App.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Http;
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

            var userServiceModel = this.users.GetUserByFacNumAndEgn(user.FacultyNumber, user.Egn);

            if (userServiceModel == null)
            {
                ModelState.AddModelError(string.Empty, "Грешно потребителско име или парола");

                return View(nameof(Index), GetQuote());
            }

            HttpContext.Session.SetInt32(SessionConstants.SessionUserId, userServiceModel.Id);
            HttpContext.Session.SetString(SessionConstants.SessionUserFirstName, userServiceModel.FirstName);
            HttpContext.Session.SetString(SessionConstants.SessionUserLastName, userServiceModel.LastName);

            var base64 = Convert.ToBase64String(userServiceModel.ProfileImage);
            var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
            HttpContext.Session.SetString(SessionConstants.SessionUserImage, imgSrc);

            return RedirectToAction("Index", "Home", new { area = "Home" });
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
