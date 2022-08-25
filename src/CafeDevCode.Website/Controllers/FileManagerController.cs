using Microsoft.AspNetCore.Mvc;

namespace CafeDevCode.Website.Controllers
{
    public class FileManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Modal()
        {
            return PartialView();
        }
    }
}
