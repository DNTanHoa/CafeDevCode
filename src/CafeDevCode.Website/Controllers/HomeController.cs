using CafeDevCode.Logic.Queries.Interface;
using CafeDevCode.Logic.Shared.Configs;
using CafeDevCode.Ultils.Model;
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
        private readonly IWebHostEnvironment environment;
        private readonly SiteConfig siteConfig;

        public HomeController(IPostQueries postQueries,
            ITagQueries tagQueries,
            ICategoryQueries categoryQueries,
            IOptions<SiteConfig> siteConfig,
            IWebHostEnvironment environment)
        {
            this.postQueries = postQueries;
            this.tagQueries = tagQueries;
            this.categoryQueries = categoryQueries;
            this.environment = environment;
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

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
