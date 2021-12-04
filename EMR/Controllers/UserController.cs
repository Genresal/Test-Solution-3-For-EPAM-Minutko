using EMR.Business.Models;
using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace EMR.Controllers
{
    public class UserController : Controller
    {
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
                var model = _pageService.GetById(id);
                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult AddOrEdit(int id, UserViewModel model)
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
                return RedirectToAction(nameof(Details), model.Id);
            }
            return View(model);
        }

        public IActionResult Details(int id = 3)
        {
            return View(_pageService.GetById(id));
        }

        public IActionResult Delete(int id)
        {
            _pageService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}