using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using EMR.ViewModels;
using EMR.Services;
using Microsoft.Extensions.Logging;
using EMR.Business.Models;
using EMR.Mapper;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EMR.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserPageService _pageService;
        private readonly ILogger<PatientsController> _logger;
        public AccountController(IUserPageService s, ILogger<PatientsController> logger)
        {
            _pageService = s;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "")        //https://newbedev.com/working-with-return-url-in-asp-net-core //ReturnUrl
        {
            if (ModelState.IsValid)
            {
                User user = _pageService.GetUserByLogin(model.Login);

                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_pageService.IsLoginExist(model.Login))
                {
                    return model.RoleId switch
                    {
                        1 => RedirectToAction("Patient", "AddOrEdit", model.ToPatient()),
                        2 => RedirectToAction("Doctor", "AddOrEdit", model.ToDoctor()),
                        _ => RedirectToAction("User", "AddOrEdit", model.ToServiceUser()),
                    };
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public IActionResult Details(int id = 3)
        {
            return View(_pageService.GetById(id).ToViewModel());
        }

        public IActionResult Delete(int id)
        {
            _pageService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}