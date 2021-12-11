﻿using EMR.Business.Models;
using EMR.Helpers;
using EMR.Models;
using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorPageService _pageService;
        private readonly ILogger<AccountController> _logger;
        private readonly AzureStorageConfig _storageConfig;

        public DoctorsController(IDoctorPageService pageService,
                                ILogger<AccountController> logger,
                                IOptions<AzureStorageConfig> storageConfig)
        {
            _pageService = pageService;
            _logger = logger;
            _storageConfig = storageConfig.Value;
        }
        [Authorize(Roles = "Doctor, Editor, Admin")]
        public IActionResult Index(int id = 0)
        {
            if (HttpContext.User.IsInRole("User"))
            {
                return RedirectToAction(nameof(Index), "Users");
            }

            if (id == 0)
            {
                string login = HttpContext.User.Identity.Name;
                return View(_pageService.GetByLogin(login));
            }

            return View(_pageService.GetById(id));
        }

        [Authorize]
        [Authorize(Roles = "Doctor, Editor, Admin")]
        public IActionResult Details(int userId)
        {
            var model = _pageService.GetByUserId(userId);

            if (User.IsInRole("Doctor"))
            {
                int currentUserId;
                if (int.TryParse(User.FindFirst("UserId").Value, out currentUserId))
                {
                    model.isUserAllowedToEdit = currentUserId == userId;
                }

                return View(model);
            }

            if (User.IsInRole("Admin"))
            {
                model.isUserAllowedToEdit = true;
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create(DoctorEditViewModel model = null)
        {
            if (model == null)
            {
                model = new DoctorEditViewModel();
            }

            model.Birthday = DateTime.Now;
            model.Positions = _pageService.PreparePositions();
            return View("AddOrEdit", model);
        }

        [Authorize(Roles = "Doctor, Admin")]
        public IActionResult Update(int id)
        {
            var model = _pageService.GetByIdEditModel(id);
            if (model == null)
            {
                return NotFound();
            }

            return View("AddOrEdit", model);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor, Admin")]
        public IActionResult AddOrEdit(DoctorEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    try
                    {
                        _pageService.Create(model);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{User.Identity.Name} failed to create new user. {ex.Message}");
                    }
                }
                else
                {
                    try
                    {
                        _pageService.Update(model);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{User.Identity.Name} failed to update user {model.Login}. {ex.Message}");
                        return NotFound();
                    }
                }

                var userId = _pageService.GetByLogin(model.Login).Id;

                return RedirectToAction(nameof(Index), new { id = userId});
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = _pageService.GetById(id).UserId;
                _pageService.Delete(id);
                _ = StorageHelper.DeleteFileFromStorage(userId.ToString(), _storageConfig);
                _logger.LogInformation($"{User.Identity.Name} deleted user with id {id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{User.Identity.Name} failed to delete user with id {id}. {ex.Message}");
            }
            return RedirectToAction(nameof(Index), "Users");
        }
    }
}
