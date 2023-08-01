using Microsoft.AspNetCore.Mvc;
using VMotion.Models;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCountriesController : Controller
    {
        private readonly dbVMotionContext _context;

        public AdminCountriesController(dbVMotionContext context)
        {
            _context = context;
        }   


        public IActionResult Index()
        {
            return View();
        }

        // Lấy all danh sách quốc gia để sử dụng cho thẻ select 

        public JsonResult AllCountry()
        {
            try
            {
                var allCountry = (from mn in _context.TblCountries.Where(x => x.IsActive == true)
                               select new
                               {
                                   CountryId = mn.CountryId,
                                   CountryName = mn.CountryName
                               }).ToList();



                return Json(new { code = 200, allCountry = allCountry, msg = "Lấy danh sách thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách thất bại: " + ex.Message });
            }
        }
    }
}
