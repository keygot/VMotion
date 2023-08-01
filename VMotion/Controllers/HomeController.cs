using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using VMotion.Models;

namespace VMotion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly dbVMotionContext _context;

        public HomeController(ILogger<HomeController> logger, dbVMotionContext context)
        {
            _logger = logger;
            _context = context;
        }

       

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Hiển thị chi tiết phim
        //// Tạo action Details
        [Route("/list-{slug}-{id:int}.html", Name = "Details")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }

            var movie = (from p in _context.TblMovies
                         join c in _context.TblCategories on p.CategoryId equals c.CategoryId
                         where p.MovieId == id && p.IsActive == true
                         orderby p.MovieId
                         select new ViewMoive
                         {
                             MovieId = p.MovieId,
                             MovieName = p.MovieName,
                             Video = p.Video,
                             Trailer = p.Trailer,
                             ImgMin = p.ImgMin,
                             ImgMax = p.ImgMax,
                             ImgBG = p.ImgBG,
                             RankIMG = p.RankIMG,
                             RankNumber = p.RankNumber,
                             Comingsoon = p.Comingsoon,
                             PosterIMG = p.PosterIMG,
                             SView = p.SView,
                             ComingsoonDate = p.ComingsoonDate,
                             ReleaseDate = p.ReleaseDate,
                             CategoryName = c.CategoryName,
                             Description = p.Description,
                             MovieActors = _context.TblActorMovies
                                .Include(am => am.Actor)
                                .Where(am => am.MovieId == id)
                                .ToList(),
                             MovieGenres = _context.TblMovieGenres
                                .Include(mg => mg.Genre)
                                .Where(mg => mg.MovieId == id)
                                .ToList(),

                             MovieDirectors = _context.TblDirectorMovies
                                .Include(md => md.Director)
                                .Where(md => md.MovieId == id)
                                .ToList(),

                             MovieEpisodes = _context.TblEpisodes
                                .Include(me => me.Movie)
                                .Where(me => me.MovieId == id)
                                .ToList(),
                         }).FirstOrDefault();


            var SviewMovie = _context.TblMovies.FirstOrDefault(p => p.MovieId == id);

            if (SviewMovie.SView != null)
            {
                SviewMovie.SView++;
            }
            else
            {
                SviewMovie.SView = 0;
            }

            _context.Update(SviewMovie);
            _context.SaveChanges();


            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }


        // Tạo list phim khi click menu tương ứng

        // Tạo action list 
        [Route("/movies-{slug}-{id:int}.html", Name = "List")]

        public IActionResult List(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var list = from c in _context.TblCategories
                       join mn in _context.TblMenus on c.CategoryId equals mn.CategoryId
                       where c.IsActive == true && mn.MenuId == id
                       orderby c.CategoryId
                       select new ViewCategory
                       {
                           //CategoryId = c.CategoryId,
                           CategoryName = c.CategoryName,
                           Movies = (from p in _context.TblMovies
                                     //join d in _context.TblDirectors on p.DirectorId equals d.DirectorId
                                     //join a in _context.TblActors on p.ActorId equals a.ActorId

                                     where p.CategoryId == c.CategoryId && p.IsActive == true
                                     orderby p.ComingsoonDate
                                     select new ViewMoive
                                     {
                                         MovieId = p.MovieId,
                                         MovieName = p.MovieName,
                                         CategoryName = c.CategoryName,
                                         RankIMG = p.RankIMG,
                                         RankNumber = p.RankNumber,
                                         Comingsoon = p.Comingsoon,
                                         PosterIMG = p.PosterIMG,
                                         //ActorId = a.ActorId,
                                         //FullName = d.FullName,
                                         ComingsoonDate = p.ComingsoonDate,
                                         Description = p.Description,
                                         ImgMin = p.ImgMin,
                                         ImgMax = p.ImgMax,
                                         ImgBG = p.ImgBG,
                                         MovieActors = _context.TblActorMovies
                                            .Include(am => am.Actor)
                                            .Where(am => am.MovieId == id)
                                            .ToList(),
                                         MovieGenres = _context.TblMovieGenres
                                            .Include(mg => mg.Genre)
                                            .Where(mg => mg.MovieId == id)
                                            .ToList(),

                                         MovieDirectors = _context.TblDirectorMovies
                                            .Include(md => md.Director)
                                            .Where(md => md.MovieId == id)
                                            .ToList(),

                                     }).ToList()
                       };
            var listOfCategoriesWithMovies = list.Where(c => c.Movies.Count > 0);
            if (listOfCategoriesWithMovies == null)
            {
                return NotFound();
            }
            return View(listOfCategoriesWithMovies.ToList());
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}