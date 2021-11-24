using EMR.Helpers;
using EMR.DataTables;
using EMR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EMR.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: HomeController
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SetConnectionString(string connectionString)
        {
            _configuration["Database"] = connectionString;
            return RedirectToAction(nameof(Index));
        }
    }
}
