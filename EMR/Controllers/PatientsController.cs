﻿using EMR.Helpers;
using EMR.DataTables;
using EMR.Business.Models;
using EMR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using EMR.Business.Services;
using EMR.Services;
using Microsoft.Extensions.Logging;
using EMR.Mapper;
using AutoMapper;

namespace EMR.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientPageService _pageService;
        private readonly ILogger<PatientsController> _logger;
        private readonly IMapper _mapper;

        public PatientsController(IPatientPageService s, ILogger<PatientsController> logger, IMapper mapper)
        {
            _pageService = s;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult LoadTable([FromBody] PatientSearchModel SearchParameters)
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

        public IActionResult LoadPatientInfoTable([FromBody] PatientSearchModel SearchParameters)
        {
            var result = _mapper.Map<List<PatientInfo>, List<PatientInfoViewModel>>(_pageService.LoadPatientInfoTable(SearchParameters).ToList());

            var filteredResultsCount = result.Count;
            var totalResultsCount = result.Count;
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

        public IActionResult Details(int id = 0)
        {
            if (id ==  0 && HttpContext.User.Identity.IsAuthenticated)
            {
                string login = HttpContext.User.Identity.Name;
                return View(_pageService.GetByLogin(login).ToViewModel());
            }

            return View(_pageService.GetById(id).ToViewModel());
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
        public IActionResult AddOrEdit(int id, [Bind("Id,Job,Login,Password,UserId,RoleId,FirstName,LastName,Birthday,PhoneNumber,Email,PhotoUrl")] PatientViewModel model)
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
                return RedirectToAction(nameof(Details), model);
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
