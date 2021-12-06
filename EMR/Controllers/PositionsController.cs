using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EMR.Controllers
{
    public class PositionsController : Controller
    {
        private readonly IPositionPageService _pageService;


        public PositionsController(IPositionPageService pageService)
        {
            _pageService = pageService;
        }

        public IActionResult Index()
        {
            return View();
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
                }
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

            return View(model);
        }

        // GET: HomeController/Delete/5
        public IActionResult Delete(int id)
        {
            if (_pageService.IsPositionInUse(id))
            {
                ViewBag.Message = "Cannot be deleted, remove the position at doctors first!";
                return View(nameof(Index));
            }

            _pageService.Delete(id);
            return View(nameof(Index));
        }
    }
}
