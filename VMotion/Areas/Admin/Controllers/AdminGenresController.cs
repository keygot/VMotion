using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VMotion.Models;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminGenresController : Controller
    {
        private readonly dbVMotionContext _context;

        public AdminGenresController(dbVMotionContext context)
        {
            _context = context;
        }   

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
           

            

            // Lấy danh sách danh mục
            var MovieList = (from role in _context.TblMovies
                                where role.IsActive == true
                                select new SelectListItem()
                                {
                                    Text = role.MovieName,
                                    Value = role.MovieId.ToString(),
                                }).ToList();
            MovieList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn phim ----",
                Value = "0"
            });

            
            ViewBag.MovieList = MovieList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TblMovie model)
        {
            if (ModelState.IsValid)
            {



                _context.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
