using Microsoft.AspNetCore.Mvc;
using MyLMSProject.Data;

namespace MyLMSProject.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly LmsDbContext _db;

        public MenuViewComponent(LmsDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var menuItems = _db.Menus.ToList();
            return View(menuItems);
        }
    }
}
