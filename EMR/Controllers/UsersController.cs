using EMR.DataTables;
using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace EMR.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserPageService _pageService;
        private readonly ILogger<PatientsController> _logger;
        public UsersController(IUserPageService s, ILogger<PatientsController> logger)
        {
            _pageService = s;
            _logger = logger;
        }

        public IActionResult Index()
        {
            UserSearchModel searchModel = new UserSearchModel();
            searchModel.Roles = _pageService.GetRoles()
                                .Select(x => new FilterCondition(x.Id, x.Name))
                                .ToList();

            return View(searchModel);
        }

        public IActionResult LoadTable([FromBody] UserSearchModel SearchParameters)
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

        public IActionResult Create(UserViewModel model = null)
        {
            if (model == null)
            {
                model = new UserViewModel();
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
        public IActionResult AddOrEdit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
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
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Details(int id)
        {
            return View(_pageService.GetById(id));
        }

        public IActionResult Delete(int id)
        {
            _pageService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}