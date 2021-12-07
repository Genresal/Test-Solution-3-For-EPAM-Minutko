using EMR.Business.Models;
using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EMR.Controllers
{
    public class SickLeavesController : Controller
    {
        readonly ISickLeavePageService _pageService;

        public SickLeavesController(ISickLeavePageService pageService)
        {
            _pageService = pageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddOrEdit(int id = 0, int recordId = 0)
        {
            if (id == 0)
            {
                var model = new SickLeaveViewModel();
                model.Id = 0;
                // TODO: Remove at drugs and at procedures and fix mapping
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
                // TODO: Remove at drugs and at procedures and fix mapping
               //model.RecordId = recordId;
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult AddOrEdit(int id, SickLeaveViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
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

                return RedirectToAction("Details", "Records", new { id = model.RecordId});
            }
            return View(model);
        }

        public IActionResult Delete(int id, int recordId)
        {
            _pageService.Delete(id);
            if (recordId == 0)
            {
                return RedirectToAction("Index", "Records");
            }

            return RedirectToAction("Details", "Records", new {id = recordId});
        }
    }
}
