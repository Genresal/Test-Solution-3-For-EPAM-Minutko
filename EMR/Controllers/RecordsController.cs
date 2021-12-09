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
using Microsoft.AspNetCore.Authorization;

namespace EMR.Controllers
{

    public class RecordsController : Controller
    {
        private readonly IRecordPageService _pageService;
        private readonly IDoctorPageService _doctorService;
        private readonly ILogger<RecordsController> _logger;

        public RecordsController(IRecordPageService recordService, 
            IDoctorPageService doctorService, 
            ILogger<RecordsController> logger)
        {
            _doctorService = doctorService;
            _pageService = recordService;
            _logger = logger;
        }
        [Authorize(Roles = "Editor, Admin")]
        public IActionResult Index()
        {
            RecordSearchModel searchModel = new RecordSearchModel();
            searchModel.DoctorPositions = _pageService.GetDoctorPositions()
                                            .Select(x => new FilterCondition(x.Id, x.Name))
                                            .ToList();

            return View(searchModel);
        }

        [HttpPost]
        [Authorize]
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

        [Authorize(Roles = "Doctor, Editor, Admin")]
        public IActionResult AddOrEdit(int id = 0)
        {
            PrepareViewBag();

            if (id == 0)
            {
                var model = new RecordViewModel();
                model.Id = 0;
                if (User.IsInRole("Doctor"))
                {
                    int doctorId = 0;
                    if (Int32.TryParse(User.FindFirst("UserId")?.Value, out doctorId))
                    {
                        var doctor = _doctorService.GetByUserId(doctorId);
                        model.DoctorId = doctor.Id;
                    }
                }

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
        [Authorize(Roles = "Doctor, Editor, Admin")]
        public IActionResult AddOrEdit(int id, RecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    model.ModifiedDate = DateTime.Now;
                    _pageService.Create(model);
                    _logger.LogInformation($"{User.Identity.Name} created new record with diagnosis {model.Diagnosis}.");
                }
                else
                {
                    try
                    {
                        _pageService.Update(model);
                        _logger.LogInformation($"{User.Identity.Name} updated record with id {model.Id}.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{User.Identity.Name} failed to update record with id {model.Id}. {ex.Message}");
                        return NotFound();
                    }
                }

                return RedirectToAction(nameof(Details), new { id = model.Id});
            }
            PrepareViewBag();
            return View(model);
        }

        [Authorize(Roles = "Doctor, Editor, Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _pageService.Delete(id);
                _logger.LogInformation($"{User.Identity.Name} deleted record with id {id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{User.Identity.Name} failed to delete record with id {id}. {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult Details(int id)
        {
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
