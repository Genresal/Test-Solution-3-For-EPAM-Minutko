﻿using EMR.Business.Models;
using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace EMR.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientPageService _pageService;
        private readonly ILogger<PatientsController> _logger;


        public PatientsController(IPatientPageService s, ILogger<PatientsController> logger)
        {
            _pageService = s;
            _logger = logger;

        }

        [HttpPost]
        public IActionResult LoadTable([FromBody] PatientSearchModel SearchParameters)
        {
            var result = _pageService.LoadTable(SearchParameters);

            var filteredResultsCount = result.Count();
            var totalResultsCount = result.Count();
            return Json(new
            {
                draw = SearchParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                .Skip(SearchParameters.Start)
                .Take(SearchParameters.Length)
            });
        }

        public IActionResult LoadPatientInfoTable([FromBody] PatientInfoSearchModel SearchParameters)
        {
            var result = _pageService.LoadPatientInfoTable(SearchParameters);

            var filteredResultsCount = result.Count();
            var totalResultsCount = result.Count();
            return Json(new
            {
                draw = SearchParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                .Skip(SearchParameters.Start)
                .Take(SearchParameters.Length)
            });
        }

        public IActionResult Details(int id = 0)
        {
            if (id == 0 && HttpContext.User.Identity.IsAuthenticated)
            {
                string login = HttpContext.User.Identity.Name;
                return View(_pageService.GetByLogin(login));
            }

            return View(_pageService.GetById(id));
        }

        public IActionResult Create(PatientViewModel model = null)
        {
            if (model == null)
            {
                model = new PatientViewModel();
            }
            return AddOrEdit(model);
        }

        public IActionResult Update(int id)
        {
            var model = _pageService.GetById(id);
            return AddOrEdit(model);
        }

        public IActionResult AddOrEdit(PatientViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }
            return View("AddOrEdit", model);
        }

        [HttpPost]
        public IActionResult AddOrEdit(int id, PatientViewModel model)
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

        // GET: HomeController/Delete/5
        public IActionResult Delete(int id)
        {
            _pageService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
