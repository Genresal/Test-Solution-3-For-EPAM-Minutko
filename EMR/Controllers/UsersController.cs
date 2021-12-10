using EMR.DataTables;
using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace EMR.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserPageService _pageService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserPageService s, ILogger<UsersController> logger)
        {
            _pageService = s;
            _logger = logger;
        }

        [Authorize(Roles = "Editor, Admin")]
        public IActionResult Index()
        {
            UserSearchModel searchModel = new UserSearchModel();
            searchModel.Roles = _pageService.GetRoles()
                                .Select(x => new FilterCondition(x.Id, x.Name))
                                .ToList();

            return View(searchModel);
        }

        [Authorize(Roles = "Editor, Admin")]
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

        [Authorize(Roles = "Admin")]
        public IActionResult Create(UserViewModel model = null)
        {
            if (model == null)
            {
                model = new UserViewModel();
            }
            return View("AddOrEdit", model);
        }

        [Authorize(Roles = "Editor, Admin")]
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
        [Authorize(Roles = "Editor, Admin")]
        public IActionResult AddOrEdit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {

                    _pageService.Create(model);
                    _logger.LogInformation($"{User.Identity.Name} created new user {model.Login}.");
                }
                else
                {
                    try
                    {
                        _pageService.Update(model);
                        _logger.LogInformation($"{User.Identity.Name} updated user with id {model.Id}.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{User.Identity.Name} failed to update user with id {model.Id}. {ex.Message}");
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize]
        public IActionResult Details(int id, int roleId = 0)
        {
            switch (roleId)
            {
                case 1:
                    return RedirectToAction("Index", "Patients", new { id = _pageService.GePatientByUserId(id).Id });
                case 2:
                    return RedirectToAction("Index", "Doctors", new { id = _pageService.GeDoctorByUserId(id).Id });
                default:
                    var model = _pageService.GetById(id);
                    if (model == null)
                    {
                        return NotFound();
                    }

                    return View("Details", model);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var model = _pageService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            if (model.RoleId > 2)
            {
                try
                {
                    _pageService.Delete(id);
                    _logger.LogInformation($"{User.Identity.Name} deleted user with id {id}.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{User.Identity.Name} failed to delete user with id {id}. {ex.Message}");
                }
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