using CafeDevCode.Logic.Queries.Interface;
using CafeDevCode.Logic.Shared.Configs;
using CafeDevCode.Ultils.Extensions;
using CafeDevCode.Ultils.Model;
using CafeDevCode.Website.Models;
using FluentEmail.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CafeDevCode.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostQueries postQueries;
        private readonly ITagQueries tagQueries;
        private readonly ICategoryQueries categoryQueries;
        private readonly MailConfig mailConfig;
        private readonly IWebHostEnvironment environment;
        private readonly IFluentEmail fluentEmail;
        private readonly SiteConfig siteConfig;

        public HomeController(IPostQueries postQueries,
            ITagQueries tagQueries,
            ICategoryQueries categoryQueries,
            IOptions<SiteConfig> siteConfig,
            IOptions<MailConfig> mailConfig,
            IWebHostEnvironment environment,
            IFluentEmail fluentEmail)
        {
            this.postQueries = postQueries;
            this.tagQueries = tagQueries;
            this.categoryQueries = categoryQueries;
            this.mailConfig = mailConfig.Value;
            this.environment = environment;
            this.fluentEmail = fluentEmail;
            this.siteConfig = siteConfig.Value;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult SiteMap()
        {
            string siteMapPath = string.Empty;
            List<SiteMapLocation> siteMapLocations = new List<SiteMapLocation>();
            var siteMap = new SiteMap();

            var posts = postQueries.GetAll();

            posts.ForEach(x =>
            {
                var siteMapLocation = new SiteMapLocation()
                {
                    Url = $"{siteConfig.HttpsUrl}{x.UrlMeta}-{x.Id}.html",
                    ChangeFrequency = SiteMapLocation.eChangeFrequency.daily,
                    LastModified = x.LastUpdatedAt.GetValueOrDefault().ToString("yyyy-MM-ddTHH:mm:ss+00:00") ?? String.Empty,
                    Priority = 1
                };

                siteMap.Add(siteMapLocation);
            });


            var directory = environment.WebRootPath + siteConfig.SiteMapDirectory;

            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            siteMapPath = directory + siteConfig.SiteMapPath;
            siteMap.WriteSiteMapToFile(siteMapPath);

            return Content(System.IO.File.ReadAllText(siteMapPath), "text/xml");
        }

        [AllowAnonymous]
        public IActionResult Contact(ContactViewModel model)
        {
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ContactSubmit(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var message = $"Người gửi: {model.Name} \r\nEmail: {model.Email} \r\n{model.Message}";
                await fluentEmail.To(mailConfig.DefaultToMailAddress)
                    .Subject(model.Subject)
                    .Body(message).SendAsync();
            }
            else
            {
                model.ErrorMessage = ModelState.GetError();
                return View("~/Views/Home/Contact.cshtml", model);
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }
    }
}
