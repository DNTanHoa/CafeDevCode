using CafeDevCode.Logic.Queries.Implement;
using CafeDevCode.Logic.Queries.Interface;
using CafeDevCode.Logic.Shared.Models;
using CafeDevCode.Website.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CafeDevCode.Website.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleQueries roleQueries;
        private readonly IMediator mediator;

        public RoleController(IRoleQueries roleQueries,
            IMediator mediator)
        {
            this.roleQueries = roleQueries;
            this.mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(string? Id = "")
        {
            var model = new RoleDetailModel();

            if (!string.IsNullOrEmpty(Id))
            {
                model = roleQueries.GetDetail(Id);
            }

            return View(model);
        }

        public async Task<ActionResult> SaveChange(RoleDetailViewModel model)
        {

        }

        public IActionResult List()
        {
            var model = new List<RoleSummaryModel>();
            model = roleQueries.GetAll();
            return PartialView(model);
        }
    }
}
