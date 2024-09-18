using Microsoft.AspNetCore.Mvc;
using MyLMSProject.Data;

namespace MyLMSProject.ViewComponents
{
    public class ArticleViewComponent : ViewComponent
    {
        private LmsDbContext db;
        public ArticleViewComponent(LmsDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            return View(db.Articles);
        }
    }
}
