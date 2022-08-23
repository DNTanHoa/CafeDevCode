using CafeDevCode.Common.Shared.Model;
using CafeDevCode.Database.Entities;
using CafeDevCode.Logic.Commands.Request;
using CafeDevCode.Logic.Queries.Interface;
using CafeDevCode.Logic.Shared.Models;
using CafeDevCode.Ultils.Extensions;
using CafeDevCode.Ultils.Global;
using CafeDevCode.Website.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeDevCode.Website.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagQueries tagQueries;
        private readonly IMediator mediator;

        public TagController(ITagQueries tagQueries,
            IMediator mediator)
        {
            this.tagQueries = tagQueries;
            this.mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Detail(int Id)
        {
            var model = new TagDetailModel();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult List()
        {
            var model = new List<TagSummaryModel>();
            model = tagQueries.GetAll();
            return PartialView(model);
        }

        public async Task<ActionResult> SaveChange(TagDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.SetBaseFromContext(HttpContext);
                var commandResult = new BaseCommandResultWithData<Tag>();

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
            var command = new DeleteTag()
            {
                Id = Id,
                RequestId = HttpContext.Connection?.Id,
                IpAddress = HttpContext.Connection?.RemoteIpAddress?.ToString(),
                UserName = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == "UserName")?.Value,
            };
            var result = await mediator.Send(command);
            return Json(new { success = result.Success, message = result.Messages });
        }
    }
}
