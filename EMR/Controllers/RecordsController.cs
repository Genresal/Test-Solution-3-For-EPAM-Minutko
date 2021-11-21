using ERM.Helpers;
using ERM.DataTables;
using ERM.Models;
using ERM.Repositories;
using ERM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ERM.Controllers
{
    public class RecordsController : Controller
    {
        RecordsRepository _recordsRepository;
        IRepository<Doctor> _doctorsRepository;
        IRepository<Patient> _patientsRepository;
        IRepository<SickLeave> _sickLeavesRepository;
        IRepository<Treatment> _treatmentsRepository;

        public RecordsController(RecordsRepository r
                                , IRepository<Doctor> rr
                                , IRepository<Patient> rrr
                                , IRepository<SickLeave> rrrr
                                , IRepository<Treatment> rrrrr)
        {
            _recordsRepository = r;
            _doctorsRepository = rr;
            _patientsRepository = rrr;
            _sickLeavesRepository = rrrr;
            _treatmentsRepository = rrrrr;
        }
        // GET: HomeController
        public ActionResult Index()
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

        public IQueryable<RecordViewModel> Filtering(RecordSearchModel searchParameters)
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

            var result = _recordsRepository.GetAllViewModel().AsQueryable();


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
                result = result.Where(u => u.ModifyingDate >= date1);

            if (date2 != DateTime.MinValue)
                result = result.Where(u => u.ModifyingDate <= date2);

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Diagnosis != null && r.Diagnosis.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, TableOrder.Asc) : result.OrderByDynamic(orderCriteria, TableOrder.Desc);

            return result;

        }

        private void ViewBagForAddOrEdit()
        {
            List<SelectListItem> doctors = new List<SelectListItem>();
            List<SelectListItem> patients = new List<SelectListItem>();
            doctors.AddRange(_doctorsRepository.GetAll()
                    //.OrderBy(x => x.Name)
                    .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = "Dr. " + x.FirstName + " " + x.LastName }).ToList());

            patients.AddRange(_patientsRepository.GetAll()
                    .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.FirstName + " " + x.LastName }).ToList());

            ViewBag.Doctors = new SelectList(doctors, "Value", "Text");
            ViewBag.Patients = new SelectList(patients, "Value", "Text");
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            ViewBagForAddOrEdit();

            if (id == 0)
            {
                var model = new Record();
                model.Id = 0;
                return View(model);
            }
            else
            {
                var model = _recordsRepository.FindById(id);
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
                    model.ModifyingDate = DateTime.Now;
                    _recordsRepository.Create(model);
                }
                //Update
                else
                {
                    try
                    {
                        model.ModifyingDate = DateTime.Now;
                        _recordsRepository.Update(model);
                    }
                    catch (Exception)
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBagForAddOrEdit();
            return View(model);
        }

        // GET: HomeController/Details/5
        public IActionResult Details(int id)
        {
            var record = _recordsRepository.FindById(id);
            var patient = _patientsRepository.FindById(record.PatientId);
            var doctor = _doctorsRepository.FindById(record.DoctorId);
            var sickLeaves = new SickLeaveSearchModel() { RecordId = record.Id};
            var treatments = _treatmentsRepository.GetByField(nameof(Treatment.RecordId), record.DoctorId.ToString()).ToList();

            var result = new Tuple<Doctor, Patient, SickLeaveSearchModel, List<Treatment>>(doctor, patient, sickLeaves, treatments);
            return View(result);
        }

        // GET: HomeController/Delete/5
        public IActionResult Delete(int id)
        {
            _recordsRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
