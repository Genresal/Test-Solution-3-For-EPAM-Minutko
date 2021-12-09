using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EMR.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserPageService _userService;
        private readonly IAccountPageService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserPageService userPageService,
            IAccountPageService accountPageService,
            ILogger<AccountController> logger)
        {
            _userService = userPageService;
            _accountService = accountPageService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserViewModel user = _userService.LogIn(model);

                if (user != null)
                {
                    await Authenticate(user);

                    _logger.LogInformation($"{User.Identity.Name} is logged into the account.");

                    return user.RoleId switch
                    {
                        1 => RedirectToAction("Index", "Patients"),
                        2 => RedirectToAction("Index", "Doctors"),
                        _ => RedirectToAction("Index", "Users"),
                    };
                }

                model.Message = "Error, incorrect login or password";
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginRandom(int roleId)
        {
            var user = _userService.GetRandomAccount(roleId);
            if (user != null)
            {
                await Authenticate(user);

                return user.RoleId switch
                {
                    1 => RedirectToAction("Index", "Patients"),
                    2 => RedirectToAction("Index", "Doctors"),
                    _ => RedirectToAction("Index", "Users"),
                };
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _logger.LogInformation($"{User.Identity.Name} is logged out.");
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            PrepareViewBag();
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_accountService.IsLoginExist(model.Login))
                {
                    return model.RoleId switch
                    {
                        1 => RedirectToAction("Create", "Patients", new { Login = model.Login, Password = model.Password, RoleId = model.RoleId }),
                        2 => RedirectToAction("Create", "Doctors", new { Login = model.Login, Password = model.Password, RoleId = model.RoleId }),
                        _ => RedirectToAction("Create", "Users", new { Login = model.Login, Password = model.Password, RoleId = model.RoleId }),
                    };
                }

                model.Message = "Error, the login already exists";
            }
            PrepareViewBag();
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            var user = _userService.GetByLogin(User.Identity.Name);
            var model = new ChangePasswordViewModel();
            if (user == null)
            {
                model.Message = $"Error, unable to load user with Login {User.Identity.Name}";
            }

            model = new ChangePasswordViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.GetByLogin(User.Identity.Name);
            if (user == null)
            {
                model.Message = $"Error, unable to load user with Login '{User.Identity.Name}'.";
                return View(model);
            }

            var changePasswordResult = _userService.ChangePassword(model, User.Identity.Name);
            if (!changePasswordResult)
            {
                model.Message = "Error, wrong current password";
                return View(model);
            }

            _logger.LogInformation($"User: {User.Identity.Name} changed his password successfully.");

            return RedirectToAction(nameof(ChangePassword));
        }

        private async Task Authenticate(UserViewModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
                new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                new Claim("PhotoUrl", string.IsNullOrEmpty(user.PhotoUrl) ? string.Empty : user.PhotoUrl),  // TODO: default picture url
                new Claim("UserId", user.Id.ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private void PrepareViewBag()
        {
            List<SelectListItem> roles = new List<SelectListItem>();
            roles.AddRange(_userService.GetRoles()
                    .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList());
            ViewBag.Roles = new SelectList(roles, "Value", "Text");
        }
    }
}