using EMR.Helpers;
using EMR.DataTables;
using EMR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using EMR.Services;
using Microsoft.AspNetCore.Authorization;

namespace EMR.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomePageService _pageService;
        private readonly IUserPageService _userPageService;
        public HomeController(IHomePageService pageService, IUserPageService userPageService)
        {
            _pageService = pageService;
            _userPageService = userPageService;
        }

        //[Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "Patients");
            }
            else if (User.IsInRole("Doctor"))
            {
                return RedirectToAction("Index", "Doctors");
            }
            else if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Users");
            }

            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var model = _userPageService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            switch (model.RoleId)
            {
                case 1:
                    return RedirectToAction("Details", "Patients", new { userId = id });
                case 2:
                    return RedirectToAction("Details", "Doctors", new { userId = id });
                default:
                    return RedirectToAction("Details", "Users", new { id = id });
            }
        }

        public IActionResult DbPage()
        {

            var status = _pageService.GetDbStatus();
            return View(status);
        }

        public IActionResult CreateBaseDate()
        {
            _pageService.CreateDefaultDate();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DropTables()
        {
            _pageService.DropTables();
            return RedirectToAction(nameof(Index));
        }
    }
}
