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

namespace EMR.Controllers
{
    public class RecordsController : Controller
    {
        readonly IRecordService _service;

        public RecordsController(IRecordService s)
        {
            _service = s;
        }
        // GET: HomeController
        public IActionResult Index()
        {
            RecordSearchModel searchModel = new RecordSearchModel();
            //
            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody] RecordSearchModel SearchParameters)
        {
            var result = Filtering(SearchParameters);

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

        public IQueryable<Record> Filtering(RecordSearchModel searchParameters)
        {
            var searchBy = searchParameters.Search?.Value;

            // if we have an empty search then just order the results by Id ascending
            var orderCriteria = "Id";
            var orderAscendingDirection = true;

            if (searchParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = searchParameters.Columns[searchParameters.Order[0].Column].Data;
                orderAscendingDirection = searchParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }

            var result = _service.GetAll().AsQueryable();


            // Filters
            //date
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();

            if (searchParameters.DateRange != null)
            {
                if (searchParameters.DateRange.Start != "")
                    date1 = DateTime.Parse(searchParameters.DateRange.Start);

                if (searchParameters.DateRange.End != "")
                    date2 = DateTime.Parse(searchParameters.DateRange.End);
            }


            if (date1 != DateTime.MinValue)
                result = result.Where(u => u.ModifiedDate >= date1);

            if (date2 != DateTime.MinValue)
                result = result.Where(u => u.ModifiedDate <= date2);

            if (!string.IsNullOrEmpty(searchBy))
            {
                //result = result.Where(r => r.Diagnosis != null && r.Diagnosis.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, TableOrder.Asc) : result.OrderByDynamic(orderCriteria, TableOrder.Desc);

            return result;

        }

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
        /*public IActionResult Details(int id)
        {
            return View(_service.GetDetails(id));
        }*/

        // GET: HomeController/Delete/5
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private void PrepareViewBagForAddOrEdit()
        {
            /*
            List<SelectListItem> doctors = new List<SelectListItem>();
            List<SelectListItem> patients = new List<SelectListItem>();
            doctors.AddRange(_service.GetDoctors()
                    .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = "Dr. " + x.FirstName + " " + x.LastName }).ToList());

            patients.AddRange(_service.GetPatients()
                    .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.FirstName + " " + x.LastName }).ToList());

            ViewBag.Doctors = new SelectList(doctors, "Value", "Text");
            ViewBag.Patients = new SelectList(patients, "Value", "Text");
            */
        }
    }
}
