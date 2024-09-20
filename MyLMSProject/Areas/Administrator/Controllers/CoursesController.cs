using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLMSProject.Data;
using MyLMSProject.Models;

namespace MyLMSProject.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class CoursesController : Controller
    {
        private readonly LmsDbContext _context;

        public CoursesController(LmsDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/Courses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.Include(x => x.Category).ToListAsync());
        }

        // GET: Administrator/Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.Include(x => x.Category)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Administrator/Courses/Create
        public IActionResult Create()
        {
            ViewBag.Dept = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewBag.Type = new SelectList(Enum.GetValues(typeof(Course.Type)));
            return View();
        }

        // POST: Administrator/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course, IFormFile ImgFile)
        {
            if (ModelState.IsValid)
            {
                if (ImgFile != null && ImgFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/img", ImgFile.FileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await ImgFile.CopyToAsync(stream);
                    }

                    course.CourseImg = ImgFile.FileName;
                }

                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Dept = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewBag.Type = new SelectList(Enum.GetValues(typeof(Course.Type)));
            return View(course);
        }

        // GET: Administrator/Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.Dept = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewBag.Type = new SelectList(Enum.GetValues(typeof(Course.Type)));
            return View(course);
        }

        // POST: Administrator/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course, IFormFile ImgFile)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (ImgFile != null && ImgFile.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot/img", ImgFile.FileName);
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await ImgFile.CopyToAsync(stream);
                        }

                        course.CourseImg = ImgFile.FileName;
                    }


                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Administrator/Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.Type = new SelectList(Enum.GetValues(typeof(Course.Type)));
            ViewBag.Dept = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(course);
        }

        // POST: Administrator/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
