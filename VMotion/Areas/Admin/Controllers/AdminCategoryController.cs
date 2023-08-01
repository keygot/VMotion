using VMotion.Models;
using VMotion.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Web.Helpers;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCategoryController : Controller
    {
        private readonly dbVMotionContext _context;

        public AdminCategoryController(dbVMotionContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            ViewData["DanhMuc"] = new SelectList(_context.TblCategories, "CategoryId", "CategoryName");
            return View();
        }

       


        // Lấy danh sách bằng AJAX
        [HttpGet]
        public JsonResult DsCategory(int trang)
        {
            try
            {
                var pageSize = int.Parse(_context.TblPhanTrangs.SingleOrDefault(x => x.TuKhoa == "so_dong_moi_trang").GiaTri);

                var dsCategory = (from mn in _context.TblCategories.OrderByDescending(x => x.CategoryId)
                              select new
                              {
                                  CategoryId = mn.CategoryId,
                                  CategoryName = mn.CategoryName,
                                  ShortName = mn.ShortName,
                                  CategoryOrder = mn.CategoryOrder,
                                  HomeFlag = mn.HomeFlag,
                                  IsActive = mn.IsActive,
                                  Description = mn.Description,

                              }).ToList();

                var kqht = dsCategory.Skip((trang - 1) * pageSize)
                              .Take(pageSize)
                              .ToList(); // Lưu kết quả hiện tại khi phân trang


                //// Phân trang


                var soTrang = dsCategory.Count() % pageSize == 0 ? dsCategory.Count() / pageSize : dsCategory.Count() / pageSize + 1;

                return Json(new { code = 200, soTrang = soTrang, dsCategory = kqht, msg = "Lấy danh sách thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách menu thất bại: " + ex.Message });
            }
        }


        // Lấy danh sách tất cả danh mục để sử dụng cho thẻ select

        [HttpGet]
        public JsonResult AllCategory()
        {
            try
            {
                var allCategory = (from ct in _context.TblCategories.Where(x => x.IsActive == true)
                                   select new
                                   {
                                       CategoryId = ct.CategoryId,
                                       CategoryName = ct.CategoryName
                                   }).ToList();

                return Json(new { code = 200, allCategory = allCategory, msg = "Lấy danh sách thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách danh mục thất bại: " + ex.Message });
            }
        }



        // Hàm Json thêm mới
        [HttpPost]

        public JsonResult Create(string categoryName, string shortName, int categoryOrder, bool isActive, bool homeFlag, string description)
        {
            try
            {
                var tblCategory = new TblCategory();
                tblCategory.CategoryName = categoryName;
                tblCategory.ShortName = shortName;
                tblCategory.CategoryOrder = categoryOrder;
                tblCategory.IsActive = isActive;
                tblCategory.HomeFlag = homeFlag;
                tblCategory.Description = description;
               

                _context.TblCategories.Add(tblCategory);
                _context.SaveChanges();


                return Json(new { code = 200, msg = "Thêm mới thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới thất bại!. Lỗi: " + ex.Message });

            }
        }


        // XEM CHI TIẾT DANH MỤC

        [HttpGet]
        public JsonResult Details(int id)
        {
            try
            {
                var tblCategory = _context.TblCategories.FirstOrDefault(x => x.CategoryId == id);

                return Json(new { code = 200, tblCategory = tblCategory, msg = "Lấy thông tin danh mục thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin thất bại. Lỗi: " + ex.Message });
            }
        }


        // Chỉnh sửa menu

        [HttpPost]

        public JsonResult Edit(int id, string categoryName, string shortName, int categoryOrder, bool isActive, bool homeFlag, string description)
        {
            try
            {
                var tblCategory = _context.TblCategories.SingleOrDefault(x => x.CategoryId == id);
                tblCategory.CategoryName = categoryName;
                tblCategory.ShortName = shortName;
                tblCategory.CategoryOrder = categoryOrder;
                tblCategory.IsActive = isActive;
                tblCategory.HomeFlag = homeFlag;
                tblCategory.Description = description;

                //Lưu vào csdl
                _context.Update(tblCategory);
                _context.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật danh mục thành công" });

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật danh mục thất bại: " + ex.Message });
            }
        }

        /// XÓA MENU
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var tblCategory = _context.TblCategories.SingleOrDefault(x => x.CategoryId == id);

                _context.Remove(tblCategory);
                _context.SaveChanges();

                return Json(new { code = 200, msg = "Xóa danh mục thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa danh mục thất bại: " + ex.Message });
            }
        }
    }
}
