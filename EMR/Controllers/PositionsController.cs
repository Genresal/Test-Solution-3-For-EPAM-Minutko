using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace EMR.Controllers
{
    [Authorize(Roles = "Editor, Admin")]
    public class PositionsController : Controller
    {
        private readonly IPositionPageService _pageService;
        private readonly ILogger<PositionsController> _logger;

        public PositionsController(IPositionPageService pageService, ILogger<PositionsController> logger)
        {
            _pageService = pageService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new PositionSearchModel());
        }

        [HttpPost]
        public IActionResult LoadTable([FromBody] PositionSearchModel SearchParameters)
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

        public IActionResult Create(PositionViewModel model = null)
        {
            if (model == null)
            {
                model = new PositionViewModel();
            }

            return View("AddOrEdit", model);
        }

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
        public IActionResult AddOrEdit(PositionViewModel model, int id = 0)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    _pageService.Create(model);
                    _logger.LogInformation($"{User.Identity.Name} created a new position with name {model.Name}.");
                }
                else
                {
                    try
                    {
                        _pageService.Update(model);
                        _logger.LogInformation($"{User.Identity.Name} updated a position with id {id}.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{User.Identity.Name} failed to update doctor's position with id {model.Id}. {ex.Message}");
                        return NotFound();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (_pageService.IsPositionInUse(id))
            {
                string message = "Error! Cannot be deleted. Doctors with that position still exist, change their position first!";
                var positionSearchModel = new PositionSearchModel() { Message = message };
                return View(nameof(Index), positionSearchModel);
            }

            try
            {
                _pageService.Delete(id);
                _logger.LogInformation($"{User.Identity.Name} deleted position with id {id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{User.Identity.Name} failed to delete position with id {id}. {ex.Message}");
            }
            return View(nameof(Index), new PositionSearchModel());
        }
    }
}
