using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLMSProject.Data;

namespace MyLMSProject.Controllers
{
    public class InstructorController : Controller
    {
        private LmsDbContext db;
        public InstructorController(LmsDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public async Task<IActionResult> AllInstructors()
        {
            return View(await db.Instructors.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> InstructorDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await db.Instructors.Include(x =>x.Courses)
                .FirstOrDefaultAsync(x => x.InstructorId == id);

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

    }
}
