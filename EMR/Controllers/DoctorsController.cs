using EMR.Business.Models;
using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace EMR.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorPageService _pageService;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(IDoctorPageService s, ILogger<DoctorsController> logger)
        {
            _pageService = s;
            _logger = logger;
        }

        public IActionResult Details(int id = 0)
        {
            DoctorViewModel viewModel;
            if (id == 0 && HttpContext.User.Identity.IsAuthenticated)
            {
                string login = HttpContext.User.Identity.Name;
                viewModel = _pageService.GetByLogin(login);
                return View(viewModel);
            }

            viewModel = _pageService.GetById(id);
            return View(viewModel);
        }

        public IActionResult Create(DoctorViewModel model = null)
        {
            if (model == null)
            {
                model = new DoctorViewModel();
            }
            return AddOrEdit(model);
        }

        public IActionResult Update(int id)
        {
            var model = _pageService.GetById(id);
            return AddOrEdit(model);
        }

        public IActionResult AddOrEdit(DoctorViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }
            return View("AddOrEdit", model);
        }

        [HttpPost]
        public IActionResult AddOrEdit(int id, DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _pageService.Create(model);
                }
                //Update
                else
                {
                    try
                    {
                        _pageService.Update(model);
                    }
                    catch (Exception)
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Details), model);
            }
            return View(model);
        }

        // GET: HomeController/Delete/5
        public IActionResult Delete(int id)
        {
            _pageService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
