using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeDevCode.Website.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
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
