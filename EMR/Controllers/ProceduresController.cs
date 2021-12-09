using EMR.Business.Models;
using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EMR.Controllers
{

    public class ProceduresController : Controller
    {
        readonly IProcedurePageService _pageService;

        public ProceduresController(IProcedurePageService pageService)
        {
            _pageService = pageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult LoadTable([FromBody] ProcedureSearchModel SearchParameters)
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
        public IActionResult AddOrEdit(int id = 0, int recordId = 0)
        {
            if (id == 0)
            {
                var model = new ProcedureViewModel();
                model.Id = 0;
                model.RecordId = recordId;
                return View(model);
            }
            else
            {
                var model = _pageService.GetById(id);
                if (model == null)
                {
                    return NotFound();
                }
                model.RecordId = recordId;
                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Doctor, Editor, Admin")]
        public IActionResult AddOrEdit(int id, ProcedureViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
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
                return RedirectToAction("Details", "Records", new { id = model.RecordId });
            }
            return View(model);
        }

        [Authorize(Roles = "Doctor, Editor, Admin")]
        public IActionResult Delete(int id, int recordId)
        { 
            _pageService.Delete(id);
            if (recordId == 0)
            {
                return RedirectToAction("Index", "Records");
            }

            return RedirectToAction("Details", "Records", new { id = recordId });
        }
    }
}
