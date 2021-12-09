using EMR.Business.Models;
using EMR.DataTables;
using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IRecordPageService _recordPageService;

        public PatientsController(IPatientPageService s, IRecordPageService recordPageService)
        {
            _pageService = s;
            _recordPageService = recordPageService;

        }

        [Authorize]
        public IActionResult Index(int id = 0)
        {
            if (!HttpContext.User.IsInRole("User"))
            {
                return RedirectToAction(nameof(Index), "Users");
            }

            PatientViewModel model;
            if (id == 0)
            {
                string login = HttpContext.User.Identity.Name;
                model = _pageService.GetByLogin(login);
            }
            else
            {
                model = _pageService.GetById(id);
            }

            RecordSearchModel searchModel = new RecordSearchModel();
            searchModel.DoctorPositions = _recordPageService.GetDoctorPositions()
                                            .Select(x => new FilterCondition(x.Id, x.Name))
                                            .ToList();
            searchModel.PatientId = model.Id;

            ViewBag.SearchModel = searchModel;

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult LoadPatientInfoTable([FromBody] PatientInfoSearchModel SearchParameters)
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

        [Authorize]
        public IActionResult Details(int id = 0)
        {
            PatientViewModel model;
            if (id == 0)
            {
                string login = HttpContext.User.Identity.Name;
                model = _pageService.GetByLogin(login);
            }
            else
            {
                model = _pageService.GetById(id);
            }

            RecordSearchModel searchModel = new RecordSearchModel();
            searchModel.DoctorPositions = _recordPageService.GetDoctorPositions()
                                            .Select(x => new FilterCondition(x.Id, x.Name))
                                            .ToList();
            searchModel.PatientId = model.Id;

            ViewBag.SearchModel = searchModel;

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create(PatientViewModel model = null)
        {
            if (model == null)
            {
                model = new PatientViewModel();
            }

            return View("AddOrEdit", model);
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult Update(int id)
        {
            var model = _pageService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            return View("AddOrEdit", model);
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public IActionResult AddOrEdit(PatientViewModel model)
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

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _pageService.Delete(id);
            return RedirectToAction(nameof(Index), "Users");
        }
    }
}
