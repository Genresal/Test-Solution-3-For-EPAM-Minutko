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
        public HomeController(IHomePageService pageService)
        {
            _pageService = pageService;
        }

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

        public IActionResult CreateDb()
        {
            _pageService.CreateDb();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DropDb()
        {
            _pageService.DropDb();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateTables()
        {
            _pageService.CreateTables();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DropTables()
        {
            _pageService.DropTables();
            return RedirectToAction(nameof(Index));
        }
    }
}
