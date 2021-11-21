using ERM.Helpers;
using ERM.DataTables;
using ERM.Models;
using ERM.Repositories;
using ERM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ERM.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }
    }
}
