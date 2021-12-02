using EMR.Helpers;
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
    public class DoctorsController : Controller
    {
        private readonly IDoctorPageService _pageService;
        private readonly ILogger<DoctorsController> _logger;
        private readonly IMapper _mapper;

        public DoctorsController(IDoctorPageService s, ILogger<DoctorsController> logger, IMapper mapper)
        {
            _pageService = s;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Details(int id = 0)
        {
            DoctorViewModel viewModel;
            if (id ==  0 && HttpContext.User.Identity.IsAuthenticated)
            {
                string login = HttpContext.User.Identity.Name;
                viewModel = _mapper.Map<DoctorViewModel>(_pageService.GetByLogin(login));
                return View(viewModel);
            }

            viewModel = _mapper.Map<DoctorViewModel>(_pageService.GetById(id));
            return View(viewModel);
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var model = new Doctor();
                model.Id = 0;
                return View(model);
            }
            else
            {
                var model = _mapper.Map<DoctorViewModel>(_pageService.GetById(id));
                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult AddOrEdit(int id, DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _pageService.Create(_mapper.Map<Doctor>(model));
                }
                //Update
                else
                {
                    try
                    {
                        _pageService.Update(_mapper.Map<Doctor>(model));
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
