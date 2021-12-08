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

            return model.RoleId switch
            {
                1 => RedirectToAction("Update", "Patients", new { id = _pageService.GePatientByUserId(id).Id }),
                2 => RedirectToAction("Update", "Doctors", new { id = _pageService.GeDoctorByUserId(id).Id }),
                _ => View("AddOrEdit", model),
            };
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
            var model = _pageService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            return model.RoleId switch
            {
                1 => RedirectToAction("Details", "Patients", new { id = _pageService.GePatientByUserId(id).Id }),
                2 => RedirectToAction("Details", "Doctors", new { id = _pageService.GeDoctorByUserId(id).Id }),
                _ => View("Details", model),
            };
        }

        public IActionResult Delete(int id)
        {
            var model = _pageService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            if (model.RoleId > 2)
            {
                _pageService.Delete(id);
            }

            return model.RoleId switch
            {
                1 => RedirectToAction("Delete", "Patients", new { id = _pageService.GePatientByUserId(id).Id }),
                2 => RedirectToAction("Delete", "Doctors", new { id = _pageService.GeDoctorByUserId(id).Id }),
                _ => View("Details", model),
            };
        }
    }
}