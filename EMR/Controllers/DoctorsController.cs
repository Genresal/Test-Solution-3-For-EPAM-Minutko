using EMR.Business.Models;
using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorPageService _pageService;

        public DoctorsController(IDoctorPageService s)
        {
            _pageService = s;
        }
        [Authorize(Roles = "Doctor, Editor, Admin")]
        public IActionResult Index(int id = 0)
        {
            if (!HttpContext.User.IsInRole("Doctor"))
            {
                return RedirectToAction(nameof(Index), "Users");
            }

            if (id == 0)
            {
                string login = HttpContext.User.Identity.Name;
                return View(_pageService.GetByLogin(login));
            }

            return View(_pageService.GetById(id));
        }

        [Authorize]
        [Authorize(Roles = "Doctor, Editor, Admin")]
        public IActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                string login = HttpContext.User.Identity.Name;
                return View(_pageService.GetByLogin(login));
            }

            return View(_pageService.GetById(id));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create(DoctorViewModel model = null)
        {
            if (model == null)
            {
                model = new DoctorViewModel();
            }
            PrepareViewBag();
            return View("AddOrEdit", model);
        }

        [Authorize(Roles = "Doctor, Admin")]
        public IActionResult Update(int id)
        {
            var model = _pageService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            PrepareViewBag();
            return View("AddOrEdit", model);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor, Admin")]
        public IActionResult AddOrEdit(DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    _pageService.Create(model);
                }
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
                return RedirectToAction(nameof(Index));
            }
            PrepareViewBag();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _pageService.Delete(id);
            return RedirectToAction(nameof(Index), "Users");
        }

        private void PrepareViewBag()
        {
            List<SelectListItem> positions = new List<SelectListItem>();
            positions.AddRange(_pageService.GetPositions()
                    .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList());
            ViewBag.Positions = new SelectList(positions, "Value", "Text");
        }
    }
}
