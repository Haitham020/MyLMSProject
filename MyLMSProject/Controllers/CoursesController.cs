using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLMSProject.Data;
using MyLMSProject.Models;
using System;
using System.Security.Claims;

namespace MyLMSProject.Controllers
{
    public class CoursesController : Controller
    {
        private LmsDbContext db;
        

        public CoursesController(LmsDbContext _db)
        {
            db = _db;
            
        }

        #region Courses
        [HttpGet]
        public async Task<IActionResult> AllCourses()
        {
            return View(await db.Courses.OrderByDescending(x => x.CourseId).ToListAsync());
        }
        

        [HttpGet]
        public async Task<IActionResult> CourseDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await db.Courses.Include(x =>x.Instructor).Include(x => x.Category)
                .Include(x =>x.Comments!)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(m => m.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }
            var categories = db.Categories.Include(c => c.Courses).ToList();
            ViewBag.Category = categories;


            return View(course);
        }
        [HttpGet]
        public async Task<IActionResult> CourseEnroll(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var course = await db.Courses.Include(x =>x.Category).SingleOrDefaultAsync(x=>x.CourseId == id);

            var isUserEnrolled = await db.UserCourses
                .AnyAsync(x => x.UserId == userId && x.CourseId == id);
            
            ViewBag.IsUserEnrolled = isUserEnrolled;

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> CourseEnroll(int courseId)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var course = await db.Courses.FindAsync(courseId);

            if (course == null)
            {
                return NotFound();
            }
          
            var userCourse = new UserCourse
            {
                UserId = userId,
                CourseId = courseId
            };

            db.UserCourses.Add(userCourse);
            await db.SaveChangesAsync();

            return RedirectToAction("MyCourses");
        }
        [HttpGet]
        public async Task<IActionResult> Refund(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await db.Courses.FindAsync(id);
            if(course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> Refund(int courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var course = await db.Courses.FindAsync(courseId);

            if (course == null)
            {
                return NotFound();
            }

            var userCourse = new UserCourse
            {
                UserId = userId,
                CourseId = courseId
            };

            db.UserCourses.Remove(userCourse);
            await db.SaveChangesAsync();

            return RedirectToAction("MyCourses");
        }
        [HttpGet]
        public async Task<IActionResult> MyCourses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var courses = await db.UserCourses
                            .Where(x => x.UserId == userId)
                            .Select(x => x.Course)
                            .ToListAsync();

            return View(courses);
        }
        #endregion

        #region Comments

        [HttpPost]
        public async Task<IActionResult> PostComment(int courseId, string text)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var comment = new Comment
            {
                CommentText = text,
                CourseId = courseId,
                UserId = userId,
                CommentCreatedAt = DateTime.Now
            };

            db.Comments.Add(comment);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region search
        [HttpGet]
        public async Task<IActionResult> SearchedCourses(string searchQuery)
        {
            if (db.Courses == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var course = from m in db.Courses
                         select m;

            if (!String.IsNullOrEmpty(searchQuery))
            {
                course = course.Where(s => s.CourseTitle!.ToUpper().Contains(searchQuery.ToUpper()));
            }

            return View(await course.ToListAsync());
        }
        #endregion

        #region Courses in Category
        [HttpGet]
        public async Task<IActionResult> CategoryCourses(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var result = await db.Categories.Include(x => x.Courses)
                .FirstOrDefaultAsync(x => x.CategoryId == id);

            if(result == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(result);
            
        }
        [HttpPost]
        public async Task<IActionResult> CategoryCourses(int CategoryId)
        {
            var result = await db.Categories.Include(x => x.Courses)
                .FirstOrDefaultAsync(x => x.CategoryId == CategoryId);

            if (result == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(result);
        }
        #endregion
    }
}
