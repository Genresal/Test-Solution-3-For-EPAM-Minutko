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

namespace EMR.Controllers
{
    public class TreatmentController : Controller
    {
        readonly IRecordPageService _pageService;

        public TreatmentController(IRecordPageService s)
        {
            _pageService = s;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View(_pageService.GetById(id));
        }
    }
}
