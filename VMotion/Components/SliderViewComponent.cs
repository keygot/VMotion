using VMotion.Models;
using Microsoft.AspNetCore.Mvc;

namespace VMotion.Components
{
    [ViewComponent(Name = "SliderView")]
    public class SliderViewComponent :ViewComponent
    {
        private dbVMotionContext _context;

        public SliderViewComponent(dbVMotionContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var listOfSlider = (from sl in _context.TblSliders
                                where sl.IsActive
                                select sl).Take(5).ToList();
            return View("Default", listOfSlider);
        }
    }
}
