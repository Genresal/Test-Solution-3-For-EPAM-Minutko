using Electronic_Medical_Record.Helpers;
using Electronic_Medical_Record.Helpers.DataTables;
using Electronic_Medical_Record.Models;
using Electronic_Medical_Record.Repositories;
using Electronic_Medical_Record.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Electronic_Medical_Record.Controllers
{
    public class SickLeavesController : Controller
    {
        IRepository<SickLeave> _sickLeavesRepository;

        public SickLeavesController(IRepository<SickLeave> r)
        {
            _sickLeavesRepository = r;
        }
        // GET: HomeController
        public ActionResult Index()
        {
            SickLeaveSearchModel searchModel = new SickLeaveSearchModel();
            //
            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody] SickLeaveSearchModel SearchParameters)
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

        public IQueryable<SickLeave> Filtering(SickLeaveSearchModel searchParameters)
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

            IQueryable<SickLeave> result;
            if (searchParameters.RecordId == 0)
            {
                result = _sickLeavesRepository.GetAll().AsQueryable();
            }
            else 
            {
                result = _sickLeavesRepository.GetByField(nameof(SickLeave.RecordId), searchParameters.RecordId.ToString()).ToList().AsQueryable();
            }

            // Filters
            //date
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            
            if (searchParameters.DateRange != null)
            {
                if (!string.IsNullOrEmpty(searchParameters.DateRange.Start))
                    date1 = DateTime.Parse(searchParameters.DateRange.Start);

                if (!string.IsNullOrEmpty(searchParameters.DateRange.End))
                    date2 = DateTime.Parse(searchParameters.DateRange.End);
            }
            

            if (date1 != DateTime.MinValue)
                result = result.Where(u => u.StartDate >= date1);

            if (date2 != DateTime.MinValue)
                result = result.Where(u => u.StartDate <= date2);

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.DiagnosisCode != null && r.DiagnosisCode.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, TableOrder.Asc) : result.OrderByDynamic(orderCriteria, TableOrder.Desc);

            return result;

        }
    }
}
