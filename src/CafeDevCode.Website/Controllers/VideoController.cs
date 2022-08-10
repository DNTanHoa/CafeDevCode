using CafeDevCode.Common.Shared.Model;
using CafeDevCode.Database.Entities;
using CafeDevCode.Logic.Queries.Interface;
using CafeDevCode.Logic.Shared.Models;
using CafeDevCode.Website.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CafeDevCode.Website.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoQueries videoQueries;
        private readonly IMediator mediator;

        public VideoController(IVideoQueries videoQueries,
            IMediator mediator)
        {
            this.videoQueries = videoQueries;
            this.mediator = mediator;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int Id)
        {
            var model = new VideoDetailModel();
            return View(model);
        }

        public IActionResult List()
        {
            var model = new List<VideoSummaryModel>();
            model = videoQueries.GetAll();
            return PartialView(model);
        }

        public async Task<ActionResult> SaveChange(VideoDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.SetBaseFromContext(HttpContext);
                var commandResult = new BaseCommandResultWithData<Video>();

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
