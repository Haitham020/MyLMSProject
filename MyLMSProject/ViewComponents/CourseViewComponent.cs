using Microsoft.AspNetCore.Mvc;
using MyLMSProject.Data;

namespace MyLMSProject.ViewComponents
{
    public class CourseViewComponent : ViewComponent
    {
        private LmsDbContext db;
        public CourseViewComponent(LmsDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            return View(db.Courses.Take(8).ToList());
        }

    }
}
