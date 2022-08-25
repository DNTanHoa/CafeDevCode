using CafeDevCode.Logic.Shared.Configs;
using elFinder.NetCore;
using elFinder.NetCore.Drivers.FileSystem;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CafeDevCode.Website.Controllers
{
    [Route("el-finder-file-system")]
    public class FileSystemController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly FileSystemConfig fileSystemConfig;

        public FileSystemController(IWebHostEnvironment environment,
            IOptions<FileSystemConfig> fileSystemConfig)
        {
            this.environment = environment;
            this.fileSystemConfig = fileSystemConfig.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("connector")]
        public async Task<IActionResult> Connector()
        {
            var connector = GetConnector();
            return await connector.ProcessAsync(Request);
        }

        [Route("thumb/{hash}")]
        public async Task<IActionResult> Thumbs(string hash)
        {
            var connector = GetConnector();
            return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
        }

        private Connector GetConnector()
        {
            string pathRoot = fileSystemConfig.RootFolder;
            var driver = new FileSystemDriver();

            string absoluteUrl = UriHelper.BuildAbsolute(Request.Scheme, Request.Host);
            var uri = new Uri(absoluteUrl);

            string rootDirectory = Path.Combine(environment.WebRootPath, pathRoot);
            string url = $"{uri.Scheme}://{uri.Authority}/{pathRoot}/";
            string urlthumb = $"{uri.Scheme}://{uri.Authority}/el-finder-file-system/thumb/";

            var root = new RootVolume(rootDirectory, url, urlthumb)
            {
                IsReadOnly = false,
                IsLocked = false,
                Alias = "Files",
                ThumbnailSize = 100
            };

            driver.AddRoot(root);

            return new Connector(driver)
            {
                MimeDetect = MimeDetectOption.Internal
            };
        }
    }
}
