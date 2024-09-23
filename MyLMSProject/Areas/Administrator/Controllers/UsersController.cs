using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLMSProject.Data;
using MyLMSProject.Models;


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
        public async Task<IActionResult> UserDetails(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound("User ID not provided.");
            }

            // Retrieve user by ID
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Pass user details to the view
            return View(user);
        }

    }
}
