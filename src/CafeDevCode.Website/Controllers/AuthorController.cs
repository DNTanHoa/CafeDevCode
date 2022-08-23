using CafeDevCode.Common.Shared.Model;
using CafeDevCode.Database.Entities;
using CafeDevCode.Logic.Commands.Request;
using CafeDevCode.Logic.Queries.Interface;
using CafeDevCode.Logic.Shared.Models;
using CafeDevCode.Ultils.Extensions;
using CafeDevCode.Ultils.Global;
using CafeDevCode.Website.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeDevCode.Website.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorQueries authorQueries;
        private readonly IMediator mediator;

        public AuthorController(IAuthorQueries authorQueries,
            IMediator mediator)
        {
            this.authorQueries = authorQueries;
            this.mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult List()
        {
            var model = new List<AuthorSummaryModel>();
            model = authorQueries.GetAll();
            return PartialView(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Detail(int Id)
        {
            var model = new AuthorDetailModel();
            
            if(Id > 0)
            {
                model = authorQueries.GetDetail(Id);
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SaveChange(AuthorDetailViewModel model)
        {
            if(ModelState.IsValid)
            {
                model.SetBaseFromContext(HttpContext);
                var commandResult = new BaseCommandResultWithData<Author>();
                
                if(model.Id == 0)
                {
                    var createCommand = model.ToCreateCommand();
                    commandResult = await mediator.Send(createCommand);
                }
                else
                {
                    var updateCommand = model.ToUpdateCommand();
                    commandResult = await mediator.Send(updateCommand);
                }

                if(commandResult.Success)
                {
                    return Json(new { success = true, 
                        message = AppGlobal.DefaultSuccessMessage, 
                        data = commandResult.Data });
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

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int Id)
        {
            var command = new DeleteAuthor()
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
