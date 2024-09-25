using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyLMSProject.Data;
using MyLMSProject.Models;
using System.Diagnostics;

namespace MyLMSProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LmsDbContext _db;
        private UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, LmsDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var onlineCourses = _db.Courses.Count(x => x.CourseType == Course.Type.Online);
            var recordedCourses = _db.Courses.Count(x => x.CourseType == Course.Type.Recorded);

            var totalInstructors = _db.Instructors.Count();
            var totalUsers = _userManager.Users.Count();

            ViewBag.onlineCourses = onlineCourses;
            ViewBag.recordedCourses = recordedCourses;
            ViewBag.totalInstructors = totalInstructors;
            ViewBag.totalUsers = totalUsers;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
