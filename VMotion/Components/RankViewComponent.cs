using Microsoft.AspNetCore.Mvc;
using VMotion.Models;

namespace VMotion.Components
{
    // Hiển thị phim xếp hạng của web
    [ViewComponent(Name = "RankView")]
    public class RankViewComponent : ViewComponent
    {


        private readonly dbVMotionContext _context;

        public RankViewComponent(dbVMotionContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {

            //var listOfCategories = from c in _context.TblCategories

            //                       where c.HomeFlag == true && c.IsActive == true
            //                       orderby c.CategoryId
            //                       select new
            //                       {
            //                           CategoryId = c.CategoryId,
            //                           CategoryName = c.CategoryName,
            //                           Movies = (from p in _context.TblMovies
            //                                     where p.CategoryId == c.CategoryId && p.IsActive == true && p.Rank == true
            //                                     orderby p.MovieId
            //                                     select new ViewMoive
            //                                     {
            //                                         MovieId = p.MovieId,
            //                                         MovieName = p.MovieName,
            //                                         CategoryName = c.CategoryName,
            //                                         RankIMG = p.RankIMG,
            //                                         RankNumber = p.RankNumber,

            //                                     }).ToList()
            //                       };


            // var listOfCategoriesWithMovies = listOfCategories.Where(c => c.Movies.Count > 0);


            var listOfMovies = (from m in _context.TblMovies
                                where m.IsActive == true && m.Rank == true
                                orderby m.SView descending
                                select m).ToList();

                                  

            return await Task.FromResult((IViewComponentResult)View("Default", listOfMovies));
        }
    }

}
