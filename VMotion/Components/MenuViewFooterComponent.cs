using VMotion.Models;
using Microsoft.AspNetCore.Mvc;

namespace VMotion.Components
{
    [ViewComponent(Name = "MenuViewFooter")]
    public class MenuViewFooterComponent : ViewComponent
    {
        private dbVMotionContext _context;

        public MenuViewFooterComponent(dbVMotionContext context)
        {
            _context = context;
        }   

        public IViewComponentResult Invoke()
        {

            var listOfMenuFooter = (from menuP in _context.TblMenus
                                    where menuP.IsActive == true && menuP.Position == 3
                                    select menuP).ToList();

            return View("Default", listOfMenuFooter);

        }
    }
}
