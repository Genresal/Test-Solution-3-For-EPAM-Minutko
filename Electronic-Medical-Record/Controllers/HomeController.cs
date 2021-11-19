using Electronic_Medical_Record.Helpers;
using Electronic_Medical_Record.Helpers.DataTables;
using Electronic_Medical_Record.Models;
using Electronic_Medical_Record.Repositories;
using Electronic_Medical_Record.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Electronic_Medical_Record.Controllers
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
