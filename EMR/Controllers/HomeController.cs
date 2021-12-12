using EMR.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EMR.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomePageService _pageService;
        private readonly IUserPageService _userPageService;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IHomePageService pageService, IUserPageService userPageService, ILogger<HomeController> logger)
        {
            _pageService = pageService;
            _userPageService = userPageService;
            _logger = logger;
        }

        //[Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "Patients");
            }
            else if (User.IsInRole("Doctor"))
            {
                return RedirectToAction("Index", "Doctors");
            }
            else if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Users");
            }

            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var model = _userPageService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            switch (model.RoleId)
            {
                case 1:
                    return RedirectToAction("Details", "Patients", new { userId = id });
                case 2:
                    return RedirectToAction("Details", "Doctors", new { userId = id });
                default:
                    return RedirectToAction("Details", "Users", new { id = id });
            }
        }

        public IActionResult DbPage()
        {

            var status = _pageService.GetDbStatus();
            return View(status);
        }

        public IActionResult CreateBaseDate()
        {
            _pageService.CreateDefaultDate();
            _logger.LogWarning($"{User.Identity.Name} created default database data.");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DropTables()
        {
            _pageService.DropTables();
            _logger.LogWarning($"{User.Identity.Name} droped database tables.");
            return RedirectToAction(nameof(Index));
        }
    }
}
