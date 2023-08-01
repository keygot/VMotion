using Microsoft.AspNetCore.Mvc;
using VMotion.Models;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSlidersController : Controller
    {
        private readonly dbVMotionContext _context;

        public AdminSlidersController(dbVMotionContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Hiển thị danh sách slider bằng ajax
        [HttpGet]
        public JsonResult DsSlide()
        {
            try
            {
                var dsSlide = (from sl in _context.TblSliders.OrderByDescending(x => x.SliderId)
                              select new
                              {
                                  SliderId = sl.SliderId,
                                  Title = sl.Title,
                                  MovieID = sl.MovieId,
                                  SliderOrder = sl.SliderOrder,
                                  ImgBGMin = sl.ImgBgmin,
                                  CreatedDate = sl.CreatedDate.Value.ToString("dd/MM/yyyy"),
                                  IsActive = sl.IsActive
                              }).ToList();

                return Json(new { code = 200, dsSlide = dsSlide, msg = "Lấy danh sách slide thành công!" });

            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách slide thất bại. Lỗi: " + ex.Message });
            }
        }


        // HÀM THÊM MỚI SLIDE

        [HttpPost]
        public JsonResult Create(int movieId, string title, string Abstract, int sliderOrder, string imgBgmin, string imgBgmax, string imgName, bool isActive)
        {
            try
            {
                   
                
                    var tblSlider = new TblSlider();

                    tblSlider.MovieId = movieId;
                    tblSlider.Title = title;
                    tblSlider.Abstract = Abstract;
                    tblSlider.SliderOrder = sliderOrder;
                    tblSlider.ImgBgmin = imgBgmin;
                    tblSlider.ImgBgmax = imgBgmax;
                    tblSlider.ImgName = imgName;
                    tblSlider.IsActive = isActive;
                    tblSlider.CreatedDate = DateTime.Now;

                _context.TblSliders.Add(tblSlider);
                    _context.SaveChanges();

                
                return Json(new { code = 200, msg = "Thêm mới slide thành công" });


            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới slide thất bại. Lỗi: " + ex.Message });
            }
        }


        // XEM CHI TIẾT SLIDE

        [HttpGet]
        public JsonResult Details(int id)
        {
            try
            {
                var tblSlides = _context.TblSliders.FirstOrDefault(x => x.SliderId == id);

                return Json(new { code = 200, tblSlides = tblSlides, msg = "Lấy thông tin slide thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin thất bại. Lỗi: " + ex.Message });
            }
        }

        // Chỉnh sửa SLIDE

        [HttpPost]

        public JsonResult Edit(int id, int movieId, string title, string Abstract, int sliderOrder, string imgBgmin, string imgBgmax, string imgName, bool isActive)
        {
            try
            {
                var tblSliders = _context.TblSliders.SingleOrDefault(x => x.SliderId == id);
                tblSliders.MovieId = movieId;
                tblSliders.Title = title;
                tblSliders.Abstract = Abstract;
                tblSliders.SliderOrder = sliderOrder;
                tblSliders.ImgBgmin = imgBgmin;
                tblSliders.ImgBgmax = imgBgmax;
                tblSliders.ImgName = imgName;

                tblSliders.IsActive = isActive;
                tblSliders.ModifiedDate = DateTime.Now;

                //Lưu vào csdl
                _context.Update(tblSliders);
                _context.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật slide thành công" });

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật slide thất bại: " + ex.Message });
            }
        }

        /// XÓA SLIDE
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var tblSliders = _context.TblSliders.SingleOrDefault(x => x.SliderId == id);

                _context.Remove(tblSliders);
                _context.SaveChanges();

                return Json(new { code = 200, msg = "Xóa slide thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa slide thất bại: " + ex.Message });
            }
        }

    }

}
