﻿using AutoMapper;
using EMR.Business.Models;
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
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public AccountController(IUserPageService userPageService,
            IAccountPageService accountPageService,
            IMapper mapper,
            ILogger<AccountController> logger)
        {
            _userService = userPageService;
            _mapper = mapper;
            _accountService = accountPageService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> LoginRandom(int roleId)
        {
            var user = _userService.GetRandomAccount(roleId);
            if (user != null)
            {
                await Authenticate(user);
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "")        //https://newbedev.com/working-with-return-url-in-asp-net-core //ReturnUrl
        {
            if (ModelState.IsValid)
            {
                UserViewModel user = _userService.GetByLogin(model.Login);

                if (user != null)
                {
                    await Authenticate(user);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Incorrect login or password");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Register()
        {
            PrepareViewBag();
            return View();
        }

        [HttpPost]
        [Authorize]
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
                else
                    ModelState.AddModelError("", "Choose another login");
            }
            PrepareViewBag();
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var user = _userService.GetByLogin(User.Identity.Name);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with Login '{User.Identity.Name}'.");
            }

            var model = new ChangePasswordViewModel();
            return View(model);
        }

        [HttpPost]
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
                throw new ApplicationException($"Unable to load user with Login '{User.Identity.Name}'.");
            }

            var changePasswordResult = _userService.ChangePassword(model, User.Identity.Name);
            if (!changePasswordResult)
            {
                return View(model);
            }

            _logger.LogInformation($"User: {User.Identity.Name} changed their password successfully.");

            return RedirectToAction(nameof(ChangePassword));
        }

        private async Task Authenticate(UserViewModel user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
                new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                new Claim("PhotoUrl", user.PhotoUrl),
                new Claim("UserId", user.Id.ToString())
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
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