using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using EMR.ViewModels;
using EMR.Services;
using Microsoft.Extensions.Logging;
using EMR.Business.Models;
using EMR.Mapper;
using System;

namespace EMR.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserPageService _pageService;
        private readonly ILogger<PatientsController> _logger;
        public UserController(IUserPageService s, ILogger<PatientsController> logger)
        {
            _pageService = s;
            _logger = logger;
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var model = new Patient();
                model.Id = 0;
                return View(model);
            }
            else
            {
                var model = _pageService.GetById(id).ToViewModel();
                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult AddOrEdit(int id, [Bind("Id,RoleId,FirstName,LastName,Birthday,PhoneNumber,Email,PhotoUrl")] ServiceUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _pageService.Create(model.ToModel());
                }
                //Update
                else
                {
                    try
                    {
                    _pageService.Update(model.ToModel());
                    }
                    catch (Exception)
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Details), model.Id);
            }
            return View(model);
        }

        public IActionResult Details(int id = 3)
        {
            return View(_pageService.GetById(id).ToViewModel());
        }

        public IActionResult Delete(int id)
        {
            _pageService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}