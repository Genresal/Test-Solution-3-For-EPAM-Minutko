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

namespace EMR.Controllers
{
    public class UserController : Controller
    {
        //Документация
        //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-5.0
        private readonly IUserPageService _pageService;
        private readonly ILogger<PatientsController> _logger;
        public UserController(IUserPageService s, ILogger<PatientsController> logger)
        {
            _pageService = s;
            _logger = logger;
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var model = new Patient();
                model.Id = 0;
                return View(model);
            }
            else
            {
                var model = _pageService.GetById(id).ToViewModel();
                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult AddOrEdit(int id, [Bind("Id,RoleId,FirstName,LastName,Birthday,PhoneNumber,Email,PhotoUrl")] ServiceUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _pageService.Create(model.ToModel());
                }
                //Update
                else
                {
                    try
                    {
                    _pageService.Update(model.ToModel());
                    }
                    catch (Exception)
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Details), model.Id);
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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "")        //https://newbedev.com/working-with-return-url-in-asp-net-core //ReturnUrl
        {
            if (ModelState.IsValid)
            {
                User user = _pageService.GetUserByLogin(model.Login);

                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    if(!string.IsNullOrEmpty(returnUrl))
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
                new Claim(ClaimsIdentity.DefaultNameClaimType, $"{user.FirstName} {user.LastName}"),
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