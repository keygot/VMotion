using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VMotion.Models;

namespace VMotion.Components
{
    // Hiển thị phim phổ biến của web
    [ViewComponent(Name = "PopularView")]
    public class PopularViewComponent : ViewComponent
    {
        private readonly dbVMotionContext _context;

        public PopularViewComponent(dbVMotionContext context)
        {
            _context = context;
        }   

        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            

            var listOfCategories = from c in _context.TblCategories
                                   where c.HomeFlag == true && c.IsActive == true 
                                   orderby c.CategoryOrder
                                   select new ViewCategory
                                   {
                                       CategoryName = c.CategoryName,
                                       Movies = (from p in _context.TblMovies
                                                 where p.CategoryId == c.CategoryId && p.IsActive == true && p.Popular == true
                                                 orderby p.MovieId
                                                 select new ViewMoive
                                                 {
                                                     MovieId = p.MovieId,
                                                     MovieName = p.MovieName,
                                                     CategoryName = c.CategoryName,
                                                     ImgMin = p.ImgMin,
                                                     ImgBG = p.ImgBG,
                                                     ImgMax = p.ImgMax,
                                                     Popular = p.Popular,
                                                     Rank = p.Rank,
                                                     RankIMG = p.RankIMG,
                                                     RankNumber = p.RankNumber,
                                                     Comingsoon = p.Comingsoon,
                                                     PosterIMG = p.PosterIMG,
                                                     MovieGenres = _context.TblMovieGenres
                                                        .Include(mg => mg.Genre)
                                                        .Where(mg => mg.MovieId == p.MovieId)
                                                        .ToList(),
                                                     MovieActors = _context.TblActorMovies
                                                        .Include(am => am.Actor)
                                                        .Where(am => am.MovieId == id)
                                                        .ToList(),

                                                     MovieDirectors = _context.TblDirectorMovies
                                                        .Include(md => md.Director)
                                                        .Where(md => md.MovieId == id)
                                                        .ToList(),

                                                 }).ToList()
                                   };
            


            var listOfCategoriesWithMovies = listOfCategories.Where(c => c.Movies.Count > 0);



            return await Task.FromResult((IViewComponentResult)View("Default", listOfCategoriesWithMovies.ToList()));
        }
    }
}
