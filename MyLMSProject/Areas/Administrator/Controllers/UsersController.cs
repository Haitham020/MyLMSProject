using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLMSProject.Areas.Administrator.Models.ViewModels;
using MyLMSProject.Data;
using MyLMSProject.Models;
using System.Security.Claims;


namespace MyLMSProject.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class UsersController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private LmsDbContext _db;

        public UsersController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, LmsDbContext db
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> ApproveUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.Active = true;
                user.InActive = false;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeactivateUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.Active = false;
                user.InActive = true;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> UserDetails(string? id)
        {
            
            var user = await _db.Users.FindAsync(id);

            var courses = await _db.UserCourses
                            .Where(x => x.UserId == id)
                            .Select(x => x.Course!)
                            .ToListAsync();
            if(courses == null)
            {
                return NotFound();
            }
            var viewModel = new UserCoursesViewModel
            {
                User = user,
                Courses = courses
            };

            return View(viewModel);
        }


    }
}
