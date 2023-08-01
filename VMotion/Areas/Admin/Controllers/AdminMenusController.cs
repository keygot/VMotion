using VMotion.Models;
using VMotion.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Web.Helpers;
using System.Security.Cryptography.X509Certificates;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminMenusController : Controller
    {
        private readonly dbVMotionContext _context;

        public AdminMenusController(dbVMotionContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        // Lấy danh sách bằng AJAX
        [HttpGet]
        public JsonResult DsMenu(int trang, string tuKhoa)
        {
            try
            {
                var pageSize = int.Parse(_context.TblPhanTrangs.SingleOrDefault(x => x.TuKhoa == "so_dong_moi_trang").GiaTri);

                var dsMenu = (from mn in _context.TblMenus.OrderByDescending(x => x.MenuId)
                              where mn.IsActive == true 
                              select new
                              {
                                  MenuId = mn.MenuId,
                                  MenuName = mn.MenuName,
                                  Levels = mn.Levels,
                                  ParentId = mn.ParentId,
                                  MenuOrder = mn.MenuOrder,
                                  IsActive = mn.IsActive

                              }).ToList();

                var kqht = dsMenu.Skip((trang - 1) * pageSize)
                              .Take(pageSize)
                              .ToList(); // Lưu kết quả hiện tại khi phân trang


                //// Phân trang


                var soTrang = dsMenu.Count() % pageSize == 0 ? dsMenu.Count() / pageSize : dsMenu.Count() / pageSize + 1;

                return Json(new {code = 200, soTrang = soTrang, dsMenu = kqht, msg = "Lấy danh sách thành công!"});
            }catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách menu thất bại: " + ex.Message});
            }
        }


        // Lấy all danh sách menu để sử dụng cho thẻ select 

        public JsonResult AllMenu()
        {
            try
            {
                var allMenu = (from mn in _context.TblMenus.Where(x => x.IsActive == true)
                    select new
                    {
                        MenuId = mn.MenuId,
                        MenuName = mn.MenuName
                    }).ToList();

                

                return Json(new { code = 200, allMenu = allMenu, msg = "Lấy danh sách thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách thất bại: " + ex.Message });
            }
        }



        // =========================================


        // Hàm Json thêm mới
        [HttpPost]

        public JsonResult Create(string menuName, int parentId, int categoryId, int levels, int menuOrder, int position, bool isActive)
        {
            try
            {
                var tblMenus = new TblMenu();
                tblMenus.MenuName = menuName;
                tblMenus.ParentId = parentId;
                tblMenus.CategoryId = categoryId;
                tblMenus.Levels = levels;
                tblMenus.MenuOrder = menuOrder;
                tblMenus.Position = position;
                tblMenus.IsActive = isActive;
                tblMenus.CreatedDate = DateTime.Now;

                _context.TblMenus.Add(tblMenus);
                _context.SaveChanges();


                return Json(new { code = 200, msg = "Thêm mới thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới thất bại!. Lỗi: " + ex.Message });

            }
        }


        // XEM CHI TIẾT MENU

        [HttpGet]
        public JsonResult Details(int id)
        {
            try
            {
                var tblMenus = _context.TblMenus.FirstOrDefault(x => x.MenuId == id);

                return Json(new { code = 200, tblMenus = tblMenus, msg = "Lấy thông tin menu thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin thất bại. Lỗi: " + ex.Message});
            }
        }


        // Chỉnh sửa menu

        [HttpPost]

        public JsonResult Edit(int id, string menuName, int parentId, int categoryId, int levels, int menuOrder, int position, bool isActive)
        {
            try
            {
                var tblMenus = _context.TblMenus.SingleOrDefault(x => x.MenuId == id);
                tblMenus.MenuName = menuName;
                tblMenus.ParentId = parentId;
                tblMenus.CategoryId = categoryId;
                tblMenus.Levels = levels;
                tblMenus.MenuOrder = menuOrder;
                tblMenus.Position = position;
                tblMenus.IsActive = isActive;
                tblMenus.ModifiedDate = DateTime.Now;

                //Lưu vào csdl
                _context.Update(tblMenus);
                _context.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật menu thành công" });

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật menu thất bại: " + ex.Message });
            }
        }

        /// XÓA MENU
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var tblMenus = _context.TblMenus.SingleOrDefault(x => x.MenuId == id);

                _context.Remove(tblMenus);
                _context.SaveChanges();

                return Json(new { code = 200, msg = "Xóa menu thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa menu thất bại: " + ex.Message });
            }
        }
        
    }
}
