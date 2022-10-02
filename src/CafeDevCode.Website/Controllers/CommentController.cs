using CafeDevCode.Common.Shared.Model;
using CafeDevCode.Database.Entities;
using CafeDevCode.Logic.Commands.Request;
using CafeDevCode.Logic.Queries.Implement;
using CafeDevCode.Logic.Queries.Interface;
using CafeDevCode.Logic.Shared.Models;
using CafeDevCode.Ultils.Extensions;
using CafeDevCode.Website.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeDevCode.Website.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentQueries commentQueries;
        private readonly IPostQueries postQueries;
        private readonly IMediator mediator;

        public CommentController(ICommentQueries commentQueries,
            IPostQueries postQueries,
            IMediator mediator)
        {
            this.commentQueries = commentQueries;
            this.postQueries = postQueries;
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

        public async Task<ActionResult> SaveChange(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.SetBaseFromContext(HttpContext);
                var commandResult = new BaseCommandResultWithData<Comment>();

                if (model.Id == 0)
                {
                    var createCommand = model.ToCreateCommand();
                    commandResult = await mediator.Send(createCommand);
                }
                else
                {
                    var updateCommand = model.ToUpdateCommand();
                    commandResult = await mediator.Send(updateCommand);
                }

                if (commandResult.Success)
                {
                    return Json(new
                    {
                        success = true,
                        message = AppGlobal.DefaultSuccessMessage,
                        data = commandResult.Data
                    });
                }
                else
                {
                    ModelState.AddModelError("", commandResult.Messages);
                    return Json(new { success = false, message = commandResult.Messages });
                }
            }
            else
            {
                return Json(new { success = false, message = ModelState.GetError() });
            }
        }

        public async Task<ActionResult> Delete(int Id)
        {
            var command = new DeleteComment()
            {
                Id = Id,
                RequestId = HttpContext.Connection?.Id,
                IpAddress = HttpContext.Connection?.RemoteIpAddress?.ToString(),
                UserName = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == "UserName")?.Value,
            };
            var result = await mediator.Send(command);
            return Json(new { success = result.Success, message = result.Messages });
        }

        [Authorize]
        public async Task<ActionResult> CreatePostComment(CommentViewModel model)
        {
            model.SetBaseFromContext(HttpContext);
            var command = model.ToCreateCommand();
            var commandResult = new BaseCommandResultWithData<Comment>();
            commandResult = await mediator.Send(command);
            return RedirectToAction("DetailPortal", "Post", new { Id = model.PostId });
        }
    }
}
