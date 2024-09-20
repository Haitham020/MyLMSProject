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
    public class ArticlesController : Controller
    {
        private readonly LmsDbContext _context;

        public ArticlesController(LmsDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/Articles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Articles.ToListAsync());
        }

        // GET: Administrator/Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Administrator/Articles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Article article, IFormFile ImgFile)
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

                    article.ArticleImg = ImgFile.FileName;
                }

                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Administrator/Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Administrator/Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Article article, IFormFile ImgFile)
        {
            if (id != article.ArticleId)
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
                            "wwwroot/img/item", ImgFile.FileName);
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await ImgFile.CopyToAsync(stream);
                        }

                        article.ArticleImg = ImgFile.FileName;
                    }


                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ArticleId))
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
            return View(article);
        }

        // GET: Administrator/Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Administrator/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleId == id);
        }
    }
}
