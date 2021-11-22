using EMR.Helpers;
using EMR.DataTables;
using EMR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Controllers
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
