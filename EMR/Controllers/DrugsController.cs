using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace EMR.Controllers
{
    public class DrugsController : Controller
    {
        private readonly IDrugPageService _pageService;
        private readonly ILogger<DrugsController> _logger;

        public DrugsController(IDrugPageService pageService, ILogger<DrugsController> logger)
        {
            _pageService = pageService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult LoadTable([FromBody] DrugSearchModel SearchParameters)
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
                var model = new DrugViewModel();
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
        public IActionResult AddOrEdit(int id, DrugViewModel model)
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
                        _logger.LogInformation($"{User.Identity.Name} updated drug information with id {id}.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{User.Identity.Name} failed to delete drug information with id {id}. {ex.Message}");
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
            try
            {
                _pageService.Delete(id);
                _logger.LogInformation($"{User.Identity.Name} deleted drug information with id {id} inside record with id {recordId}.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{User.Identity.Name} failed to delete drug information with id {id}. {ex.Message}");
            }

            if (recordId == 0)
            {
                return RedirectToAction("Index", "Records");
            }

            return RedirectToAction("Details", "Records", new { id = recordId });
        }
    }
}
