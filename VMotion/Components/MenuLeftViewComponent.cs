using VMotion.Models;
using Microsoft.AspNetCore.Mvc;

namespace VMotion.Components
{
    [ViewComponent(Name = "MenuLeftView")]
    public class MenuLeftViewComponent : ViewComponent
    {
        private dbVMotionContext _context;

        public MenuLeftViewComponent(dbVMotionContext context)
        {
               _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var listOfMenu = (from menu in _context.TblMenus
                              where (menu.IsActive == true) && (menu.Position == 1)
                              select menu).ToList();
            return View("Default", listOfMenu);
        }
    }
}
