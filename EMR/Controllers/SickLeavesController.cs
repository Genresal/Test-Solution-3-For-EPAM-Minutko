using EMR.Business.Models;
using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace EMR.Controllers
{
    [Authorize(Roles = "Doctor, Editor, Admin")]
    public class SickLeavesController : Controller
    {
        readonly ISickLeavePageService _pageService;
        private readonly ILogger<SickLeavesController> _logger;

        public SickLeavesController(ISickLeavePageService pageService, ILogger<SickLeavesController> logger)
        {
            _pageService = pageService;
            _logger = logger;
        }

        [Authorize]
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
                model.RecordId = recordId;
                model.StartDate = DateTime.Now;
                model.FinalDate = DateTime.Now.AddDays(3);
                return View(model);
            }
            else
            {
                var model = _pageService.GetById(id);
                if (model == null)
                {
                    return NotFound();
                }

                return View(model);
            }
        }

        [HttpPost]
        public IActionResult AddOrEdit(SickLeaveViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    _pageService.Create(model);
                    _logger.LogInformation($"{User.Identity.Name} created new sick leave {model.StartDate} - {model.FinalDate}.");
                }
                else
                {
                    try
                    {
                        _pageService.Update(model);
                        _logger.LogInformation($"{User.Identity.Name} updated sick leave with id {model.Id}.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{User.Identity.Name} failed to update sick leave with id {model.Id}. {ex.Message}");
                        return NotFound();
                    }
                }

                return RedirectToAction("Details", "Records", new { id = model.RecordId});
            }
            return View(model);
        }

        public IActionResult Delete(int id, int recordId)
        {
            try
            {
                _pageService.Delete(id);
                _logger.LogInformation($"{User.Identity.Name} deleted sick leave with id {id} inside record with id {recordId}.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{User.Identity.Name} failed to delete sick leave with id {id}. {ex.Message}");
            }

            if (recordId == 0)
            {
                return RedirectToAction("Index", "Records");
            }

            return RedirectToAction("Details", "Records", new {id = recordId});
        }
    }
}
