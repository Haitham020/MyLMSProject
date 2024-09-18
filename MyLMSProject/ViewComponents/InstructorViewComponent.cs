using Microsoft.AspNetCore.Mvc;
using MyLMSProject.Data;

namespace MyLMSProject.ViewComponents
{
    public class InstructorViewComponent : ViewComponent
    {
        private LmsDbContext db;
        public InstructorViewComponent(LmsDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            return View(db.Instructors.Take(10).ToList());
        }
    }
}
