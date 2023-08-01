using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VMotion.Models;

namespace VMotion.Components
{
    [ViewComponent(Name = "ComingsoonView")]
    public class ComingsoonViewComponent : ViewComponent
    {
        private readonly dbVMotionContext _context;
        public ComingsoonViewComponent(dbVMotionContext context)
        {
            _context = context;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {

            //var listOfCategories = from c in _context.TblCategories

            //                       where c.HomeFlag == true && c.IsActive == true
            //                       orderby c.CategoryId
            //                       select new ViewCategory
            //                       {
            //                           CategoryName = c.CategoryName,
            //                           Movies = (from p in _context.TblMovies
            //                                     where p.CategoryId == c.CategoryId && p.IsActive == true && p.Comingsoon == true
            //                                     orderby p.ComingsoonDate 
            //                                     select new ViewMoive
            //                                     {
            //                                         MovieId = p.MovieId,
            //                                         MovieName = p.MovieName,
            //                                         CategoryName = c.CategoryName,
            //                                         RankIMG = p.RankIMG,
            //                                         RankNumber = p.RankNumber,
            //                                         Comingsoon = p.Comingsoon,
            //                                         PosterIMG = p.PosterIMG,
            //                                         ImgMin = p.ImgMin,
            //                                         MovieActors = _context.TblActorMovies
            //                                            .Include(am => am.Actor)
            //                                            .Where(am => am.MovieId == p.MovieId)
            //                                            .ToList(),
            //                                         MovieGenres = _context.TblMovieGenres
            //                                            .Include(mg => mg.Genre)
            //                                            .Where(mg => mg.MovieId == p.MovieId)
            //                                            .ToList(),

            //                                         MovieDirectors = _context.TblDirectorMovies
            //                                            .Include(md => md.Director)
            //                                            .Where(md => md.MovieId == p.MovieId)
            //                                            .ToList(),
            //                                         ComingsoonDate = p.ComingsoonDate,
            //                                         Description = p.Description,


            //                                     }).ToList()
            //                       };
            //var listOfCategoriesWithMovies = listOfCategories.Where(c => c.Movies.Count > 0);


            var listOfComingsoon = (from p in _context.TblMovies
                                    where p.IsActive == true && p.Comingsoon == true
                                    orderby p.ComingsoonDate
                                    select new ViewMoive
                                    {
                                        MovieId = p.MovieId,
                                        MovieName = p.MovieName,

                                        RankIMG = p.RankIMG,
                                        RankNumber = p.RankNumber,
                                        Comingsoon = p.Comingsoon,
                                        PosterIMG = p.PosterIMG,
                                        ImgMin = p.ImgMin,
                                        MovieActors = _context.TblActorMovies
                                           .Include(am => am.Actor)
                                           .Where(am => am.MovieId == p.MovieId)
                                           .ToList(),
                                        MovieGenres = _context.TblMovieGenres
                                           .Include(mg => mg.Genre)
                                           .Where(mg => mg.MovieId == p.MovieId)
                                           .ToList(),

                                        MovieDirectors = _context.TblDirectorMovies
                                           .Include(md => md.Director)
                                           .Where(md => md.MovieId == p.MovieId)
                                           .ToList(),
                                        ComingsoonDate = p.ComingsoonDate,
                                        Description = p.Description,


                                    }).ToList();
                                 



            return await Task.FromResult((IViewComponentResult)View("Default", listOfComingsoon));
        }
    }
}
