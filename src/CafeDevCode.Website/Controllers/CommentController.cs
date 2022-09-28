using CafeDevCode.Database.Entities;
using CafeDevCode.Logic.Queries.Implement;
using CafeDevCode.Logic.Queries.Interface;
using CafeDevCode.Logic.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CafeDevCode.Website.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentQueries commentQueries;
        private readonly IMediator mediator;

        public CommentController(ICommentQueries commentQueries,
            IMediator mediator)
        {
            this.commentQueries = commentQueries;
            this.mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var model = new List<CommentModel>();
            model = commentQueries.GetAll();
            return PartialView(model);
        }

        public IActionResult SaveChange()
        {
            return Json();
        }
    }
}
