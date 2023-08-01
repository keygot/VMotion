using VMotion.Models;
using Microsoft.AspNetCore.Mvc;

namespace VMotion.Areas.Admin.Components
{
    [ViewComponent(Name ="AdminMenuView")]
    public class AdminMenuViewComponent : ViewComponent
    {
        private readonly dbVMotionContext _context;

        public AdminMenuViewComponent(dbVMotionContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mnList = (from mn in _context.TblAdminMenus
                          where (mn.IsActive == true)
                          select mn).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", mnList));
        }
    }
}
