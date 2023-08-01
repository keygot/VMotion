using VMotion.Models;
using Microsoft.AspNetCore.Mvc;

namespace VMotion.Components
{
    [ViewComponent(Name = "MenuMobieView")]
    public class MenuMobieViewComponent : ViewComponent
    {
        private dbVMotionContext _context;

        public MenuMobieViewComponent(dbVMotionContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var listOfMenuMobie = (from menu in _context.TblMenus
                                   where menu.IsActive == true /*&& menu.Position == 4*/
                                   select menu).ToList();

            return View("Default", listOfMenuMobie);
        }
    }
}
