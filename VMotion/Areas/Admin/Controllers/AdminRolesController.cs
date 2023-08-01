using VMotion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRolesController : Controller
    {
        private readonly dbVMotionContext _context;

        public AdminRolesController(dbVMotionContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        
        }

        // Lấy danh sách bằng AJAX
        [HttpGet]
        public JsonResult DsRole(int trang)
        {
            try
            {
                var pageSize = int.Parse(_context.TblPhanTrangs.SingleOrDefault(x => x.TuKhoa == "so_dong_moi_trang").GiaTri);

                var dsRole = (from mn in _context.TblRoles.OrderByDescending(x => x.RoleId)
                              select new
                              {
                                  RoleId = mn.RoleId,
                                  RoleName = mn.RoleName,
                                  Description = mn.Description

                              }).ToList();

                var kqht = dsRole.Skip((trang - 1) * pageSize)
                              .Take(pageSize)
                              .ToList(); // Lưu kết quả hiện tại khi phân trang


                //// Phân trang


                var soTrang = dsRole.Count() % pageSize == 0 ? dsRole.Count() / pageSize : dsRole.Count() / pageSize + 1;

                return Json(new { code = 200, soTrang = soTrang, dsRole = kqht, msg = "Lấy danh sách thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách menu thất bại: " + ex.Message });
            }
        }


        // Lấy all danh sách role để sử dụng cho thẻ select 

        public JsonResult AllRole()
        {
            try
            {
                var allRole = (from r in _context.TblRoles
                               select new
                               {
                                   RoleId = r.RoleId,
                                   RoleName = r.RoleName
                               }).ToList();



                return Json(new { code = 200, allRole = allRole, msg = "Lấy danh sách thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách thất bại: " + ex.Message });
            }
        }



        // Hàm Json thêm mới
        [HttpPost]
        public JsonResult Create(string RoleName, string Description)
        {
            try
            {
                var tblRole = new TblRole();
                tblRole.RoleName = RoleName;
                tblRole.Description = Description;

                _context.TblRoles.Add(tblRole);
                _context.SaveChanges();


                return Json(new { code = 200, msg = "Thêm mới thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới thất bại!. Lỗi: " + ex.Message });

            }
        }

        // XEM CHI TIẾT ROLE

        [HttpGet]
        public JsonResult Details(int id)
        {
            try
            {
                var tblRoles = _context.TblRoles.FirstOrDefault(x => x.RoleId == id);

                return Json(new { code = 200, tblRoles = tblRoles, msg = "Lấy thông tin role thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin thất bại. Lỗi: " + ex.Message });
            }
        }


        // Chỉnh sửa menu

        [HttpPost]

        public JsonResult Edit(int id, string RoleName, string Description)
        {
            try
            {
                var tblRoles = _context.TblRoles.SingleOrDefault(x => x.RoleId == id);
                tblRoles.RoleName = RoleName;
                tblRoles.Description = Description;
                
                //Lưu vào csdl
                _context.Update(tblRoles);
                _context.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật quyền thành công" });

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật quyền thất bại: " + ex.Message });
            }
        }

        /// XÓA MENU
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var tblRoles = _context.TblRoles.SingleOrDefault(x => x.RoleId == id);

                _context.Remove(tblRoles);
                _context.SaveChanges();

                return Json(new { code = 200, msg = "Xóa quyền thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa quyền thất bại: " + ex.Message });
            }
        }

    }
}
