using EMR.Business.Models;
using EMR.Services;
using EMR.ViewModels;
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
        // GET: HomeController
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
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

        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var model = new Procedure();
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
        public IActionResult AddOrEdit(int id, Procedure model)
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
                return RedirectToAction(nameof(Index));
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
