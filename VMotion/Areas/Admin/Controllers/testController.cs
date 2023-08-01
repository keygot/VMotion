using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VMotion.Models;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class testController : Controller
    {
        private readonly dbVMotionContext _context;
        public testController(dbVMotionContext context)
        {
            _context = context;
        }
    
        public IActionResult Index()
        {
            return View();
        }
        public class MyViewModel
        {
            public List<SelectListItem> Items { get; set; }
            public List<string> SelectedItems { get; set; }
        }

        public IActionResult Create()
        {
            //var model = new MyViewModel();
            //model.Items = new List<SelectListItem>()
            //    {
            //        new SelectListItem { Text = "Item 1", Value = "1" },
            //        new SelectListItem { Text = "Item 2", Value = "2" },
            //        new SelectListItem { Text = "Item 3", Value = "3" },
            //        new SelectListItem { Text = "Item 4", Value = "4" },

            //    };

            

            return View();
        }

        [HttpPost]
        public IActionResult Create(MyViewModel model)
        {
            // Save selected items to database
            return RedirectToAction("Index");
        }

    }
}
