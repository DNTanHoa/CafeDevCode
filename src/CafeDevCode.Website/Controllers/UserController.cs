using CafeDevCode.Common.Shared.Model;
using CafeDevCode.Database.Entities;
using CafeDevCode.Logic.Commands.Request;
using CafeDevCode.Logic.Queries.Implement;
using CafeDevCode.Logic.Queries.Interface;
using CafeDevCode.Logic.Shared.Models;
using CafeDevCode.Ultils.Extensions;
using CafeDevCode.Website.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace CafeDevCode.Website.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMediator mediator;
        private readonly IUserQueries userQueries;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public UserController(IMediator mediator, 
            IUserQueries userQueries,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userQueries = userQueries;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var model = new List<UserSummaryModel>();
            model = userQueries.GetAll();
            return PartialView(model);
        }

        public IActionResult Detail(string? userName)
        {
            var model = new UserDetailModel();

            if (!string.IsNullOrEmpty(userName))
            {
                model = userQueries.GetDetail(userName);
            }

            return View(model);
        }

        public async Task<ActionResult> SaveChange(UserDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.SetBaseFromContext(HttpContext);
                var commandResult = new BaseCommandResultWithData<User>();

                if (!userQueries.IsExistUserName(model.DetailUserName ?? string.Empty))
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AdminLogin(LoginViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ResetPasswordConfirm(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.SetBaseFromContext(HttpContext);
                var commandResult = new BaseCommandResult();
                var resetPasswordCommand = model.ToResetPasswordCommand();
                commandResult = await mediator.Send(resetPasswordCommand);

                return Json(new { success = commandResult.Success, message = commandResult.Messages });
            }
            else
            {
                return Json(new { success = false, message = ModelState.GetError() });
            }
        }

        public async Task<ActionResult> Delete(string userName)
        {
            var command = new DeleteUser()
            {
                DeleteUserName = userName,
                RequestId = HttpContext.Connection?.Id,
                IpAddress = HttpContext.Connection?.RemoteIpAddress?.ToString(),
                UserName = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == "UserName")?.Value,
            };
            var result = await mediator.Send(command);
            return Json(new { success = result.Success, message = result.Messages });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AdminLoginSubmit(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var command = model.ToCommand();
                var result = await mediator.Send(command);

                if(result.Success)
                {
                    var user = result.Data;

                    var claims = new List<Claim>()
                    {
                        new Claim("UserName", user!.UserName),
                        new Claim("AuthorId", user!.AuthorId ?? string.Empty)
                    };

                    var claimIdentities = new List<ClaimsIdentity>()
                    {
                        new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)
                    };

                    var claimPrincipal = new ClaimsPrincipal(claimIdentities);
                    await signInManager.SignInAsync(user, model.RememberPassWord);

                    if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(model.ReturnUrl);
                    }
                }
                else
                {
                    model.ErrorMessage = result.Messages;
                    return RedirectToAction("AdminLogin", model);
                }
            }
            else
            {
                model.ErrorMessage = ModelState.GetError();
                return RedirectToAction("AdminLogin", model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoginPortal(LoginViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginPortalSubmit(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = model.ToCommand();
                var result = await mediator.Send(command);

                if (result.Success)
                {
                    var user = result.Data;

                    var claims = new List<Claim>()
                    {
                        new Claim("UserName", user!.UserName),
                        new Claim("AuthorId", user!.AuthorId ?? string.Empty)
                    };

                    var claimIdentities = new List<ClaimsIdentity>()
                    {
                        new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)
                    };

                    var claimPrincipal = new ClaimsPrincipal(claimIdentities);
                    await signInManager.SignInAsync(user, model.RememberPassWord);

                    if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(model.ReturnUrl);
                    }
                }
                else
                {
                    model.ErrorMessage = result.Messages;
                    return RedirectToAction("LoginPortal", model);
                }
            }
            else
            {
                model.ErrorMessage = ModelState.GetError();
                return RedirectToAction("LoginPortal", model);
            }
        }

        [HttpGet]
        public IActionResult DetailPortal(string userName)
        {
            var model = new UserDetailModel();
            var requestUserName = HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;
            
            if(requestUserName != userName)
            {
                return RedirectToAction("AccessDenied");
            }
            else
            {
                model = userQueries.GetDetail(userName);
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterPortal(RegisterViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterPortalSubmit(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                model.SetBaseFromContext(HttpContext);
                var commandResult = new BaseCommandResult();
                var createUserCommand = model.ToCreateCommand();
                commandResult = await mediator.Send(createUserCommand);

                if (commandResult.Success)
                {
                    return RedirectToAction("LoginPortal", new { UserName = model.UserName });
                }
                else
                {
                    ModelState.AddModelError("", commandResult.Messages);
                }
            }

            model.ErrorMessage = ModelState.GetError();
            return RedirectToAction("RegisterPortal", model);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangePasswordPortal(ChangePasswordViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ChangePasswordPortalSubmit(ChangePasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var requestUserName = HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;

                if(requestUserName == model.UserName)
                {
                    model.SetBaseFromContext(HttpContext);
                    var commandResult = new BaseCommandResult();
                    var changePasswordCommand = model.ToChangePasswordCommand();
                    commandResult = await mediator.Send(changePasswordCommand);

                    if(commandResult.Success)
                    {
                        return RedirectToAction("DetailPortal", new { userName = model.UserName });
                    }
                    else
                    {
                        ModelState.AddModelError("", commandResult.Messages);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user");
                }
            }

            model.ErrorMessage = ModelState.GetError();
            return RedirectToAction("ChangePasswordPortal", model);
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleLoginResponse", "User");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleLoginResponse()
        {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if(info == null)
            {
                return RedirectToAction("LoginPortal");
            }
            else
            {
                var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    User user = new User()
                    {
                        Email = info.Principal.FindFirst(ClaimTypes.Email)?.Value,
                        UserName = info.Principal.FindFirst(ClaimTypes.Email)?.Value,
                    };

                    var createUserResult = await userManager.CreateAsync(user);

                    if(createUserResult.Succeeded)
                    {
                        var identResult = await userManager.AddLoginAsync(user, info);
                        if(identResult.Succeeded)
                        {
                            await signInManager.SignInAsync(user, false);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            return View();
        }
    }
}
