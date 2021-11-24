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

namespace EMR.Controllers
{
    public class RecordsController : Controller
    {
        readonly IRecordPageService _pageService;

        public RecordsController(IRecordPageService s)
        {
            _pageService = s;
        }
        // GET: HomeController
        public IActionResult Index()
        {
            RecordSearchModel searchModel = new RecordSearchModel();
            //
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

        /*
        public IActionResult AddOrEdit(int id = 0)
        {
            PrepareViewBagForAddOrEdit();

            if (id == 0)
            {
                var model = new Record();
                model.Id = 0;
                return View(model);
            }
            else
            {
                var model = _service.FindById(id);
                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult AddOrEdit(int id, [Bind("Id,PatientId,Diagnosis,DoctorId,SickLeaveId,ModifyData")] Record model)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _service.Create(model);
                }
                //Update
                else
                {
                    try
                    {
                        _service.Update(model);
                    }
                    catch (Exception)
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            PrepareViewBagForAddOrEdit();
            return View(model);
        }

        // GET: HomeController/Details/5
        public IActionResult Details(int id)
        {
            return View(_service.GetDetails(id));
        }

        // GET: HomeController/Delete/5
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private void PrepareViewBagForAddOrEdit()
        {
            
            List<SelectListItem> doctors = new List<SelectListItem>();
            List<SelectListItem> patients = new List<SelectListItem>();
            doctors.AddRange(_service.GetDoctors()
                    .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = "Dr. " + x.FirstName + " " + x.LastName }).ToList());

            patients.AddRange(_service.GetPatients()
                    .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.FirstName + " " + x.LastName }).ToList());

            ViewBag.Doctors = new SelectList(doctors, "Value", "Text");
            ViewBag.Patients = new SelectList(patients, "Value", "Text");
            
        }
        */
    }
}
