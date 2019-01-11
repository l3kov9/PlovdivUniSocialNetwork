namespace PuSocialNetwork.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PuSocialNetwork.App.Models;
    using System;
    using System.Diagnostics;
    using System.Net;

    public class HomeController : Controller
    {
        private const string QuotesUrl = @"http://nvp-playground.azurewebsites.net/api/quotes/random/txt";

        public IActionResult Index()
        {
            var client = new WebClient();
            var quoteAsText = client.DownloadString(QuotesUrl);

            var quoteLines = quoteAsText.Split(Environment.NewLine);

            return View(nameof(Index), quoteLines);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
