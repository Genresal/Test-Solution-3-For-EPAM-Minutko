﻿using EMR.Helpers;
using EMR.DataTables;
using EMR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using EMR.Services;

namespace EMR.Controllers
{
    public class HomeController : Controller
    {
        private IHomePageService _pageService;
        public HomeController(IHomePageService pageService)
        {
            _pageService = pageService;
        }
        // GET: HomeController
        public IActionResult Index()
        {
            var status = _pageService.GetDbStatus();
            return View(status);
        }

        public IActionResult CheckDb()
        {
            _pageService.CheckDb();
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
