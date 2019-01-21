namespace PuSocialNetwork.App.Areas.Blog.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using PuSocialNetwork.App.Infrastructure;
    using Services;
    using System;
    using System.Threading.Tasks;

    public class ArticlesController : BlogBaseController
    {
        private const int PageSize = 3;

        private readonly IBlogArticleService articles;
        private readonly IHtmlService html;


        public ArticlesController(IBlogArticleService articles, IHtmlService html)
        {
            this.articles = articles;
            this.html = html;
        }

        [Route("/Articles")]
        public async Task<IActionResult> Index(int page = 1)
            => View(new ArticleListingViewModel
            {
                Articles = await this.articles.AllAsync(page, PageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)await this.articles.TotalAsync() / PageSize),
                CurrentUserName = this.HttpContext.Session.GetString(SessionConstants.SessionUserFirstName) + " " + this.HttpContext.Session.GetString(SessionConstants.SessionUserLastName)
            });

        [Route("/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var article = await this.articles.ByIdAsync(id);

            return View(article);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(PublishArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Content = this.html.Sanitize(model.Content);
            var userId = (int)this.HttpContext.Session.GetInt32(SessionConstants.SessionUserId);

            await this.articles.CreateAsync(model.Title, model.Content, userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("DeleteArticle")]
        public async Task<IActionResult> DeleteArticle(int ArticleId, int page)
        {
            bool success = await this.articles.DeleteByIdAsync(ArticleId);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index), new { page });
        }
    }
}
