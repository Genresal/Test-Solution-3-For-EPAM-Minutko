using EMR.Helpers;
using EMR.DataTables;
using EMR.Business.Models;
using EMR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using EMR.Business.Services;
using EMR.Services;
using Microsoft.Extensions.Logging;

namespace EMR.Controllers
{
    public class RecordsController : Controller
    {
        private readonly IRecordPageService _pageService;
        private readonly ILogger<RecordsController> _logger;

        public RecordsController(IRecordPageService s, ILogger<RecordsController> logger)
        {
            _pageService = s;
            _logger = logger;
        }
        // GET: HomeController
        public IActionResult Index()
        {
            RecordSearchModel searchModel = new RecordSearchModel();
            searchModel.DoctorPositions = _pageService.GetDoctorPositions()
                                            .Select(x => new FilterCondition(x.Id, x.Name))
                                            .ToList();
            //
            _logger.LogInformation("The main page has been accessed");

            return View(searchModel);
        }

        [HttpPost]
        public IActionResult LoadTable([FromBody] RecordSearchModel SearchParameters)
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
                .ToList()
            });
        }

        
        public IActionResult AddOrEdit(int id = 0)
        {
            PrepareViewBag();

            if (id == 0)
            {
                var model = new Record();
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
        public IActionResult AddOrEdit(int id, RecordViewModel model)
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
                    //try
                    //{
                        _pageService.Update(model);
                    //}
                    //catch (Exception)
                    //{
                    //    return NotFound();
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            PrepareViewBag();
            return View(model);
        }

                // GET: HomeController/Delete/5
        public IActionResult Delete(int id)
        {
            _pageService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: HomeController/Details/5
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(_pageService.Details(id));
        }

        private void PrepareViewBag()
        {
            List<SelectListItem> doctors = new List<SelectListItem>();
            List<SelectListItem> patients = new List<SelectListItem>();
            doctors.AddRange(_pageService.GetDoctors()
                    .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = "Dr. " + x.User.FirstName + " " + x.User.LastName }).ToList());

            patients.AddRange(_pageService.GetPatients()
                    .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.User.FirstName + " " + x.User.LastName }).ToList());

            ViewBag.Doctors = new SelectList(doctors, "Value", "Text");
            ViewBag.Patients = new SelectList(patients, "Value", "Text");
        }
    }
}
