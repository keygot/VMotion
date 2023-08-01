using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Helpers;
using VMotion.Models;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminActorsController : Controller
    {
        private readonly dbVMotionContext _context;

        public AdminActorsController(dbVMotionContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        // Lấy danh sách bằng AJAX
        [HttpGet]
        public JsonResult DsActors(int trang)
        {
            try
            {
                var pageSize = int.Parse(_context.TblPhanTrangs.SingleOrDefault(x => x.TuKhoa == "so_dong_moi_trang").GiaTri);

                var dsActors = (from ac in _context.TblActors.OrderByDescending(x => x.ActorId)
                                select new
                                {
                                    ActorId = ac.ActorId,
                                    FullName = ac.FullName,
                                    Phone = ac.Phone,
                                    Email = ac.Email,
                                    Avatar = ac.Avatar,
                                    BirthDate = ac.BirthDate.Value.ToString("dd/MM/yyyy"),
                                    Gender = ac.Gender,
                                    CountryId = ac.CountryId,
                                    Address = ac.Address,
                                    Description = ac.Description,
                                    IsActive = ac.IsActive,


                                }).ToList();

                var kqht = dsActors.Skip((trang - 1) * pageSize)
                              .Take(pageSize)
                              .ToList(); // Lưu kết quả hiện tại khi phân trang


                //// Phân trang


                var soTrang = dsActors.Count() % pageSize == 0 ? dsActors.Count() / pageSize : dsActors.Count() / pageSize + 1;

                return Json(new { code = 200, soTrang = soTrang, dsActors = kqht, msg = "Lấy danh sách thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách thất bại: " + ex.Message });
            }
        }

        // XEM CHI TIẾT DIỄN VIÊN

        [HttpGet]
        public JsonResult Details(int id)
        {
            try
            {
                var tblActors = _context.TblActors.FirstOrDefault(x => x.ActorId == id);

                return Json(new { code = 200, tblActors = tblActors, msg = "Lấy thông tin diễn viên thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin thất bại. Lỗi: " + ex.Message });
            }
        }


        public JsonResult Create(string fullName, int gender, string phone, DateTime? birthDate, string avatar, string email, int countryId, bool isActive, string address)
        {
            try
            {
                var tblActors = new TblActor();
                tblActors.FullName = fullName;
                tblActors.CountryId = countryId;
                tblActors.Gender = gender;
                tblActors.Phone = phone;
                tblActors.Email = email;
                tblActors.Address = address;
                tblActors.BirthDate = birthDate;
                tblActors.Avatar = avatar;

                tblActors.IsActive = isActive;


                tblActors.CreatedDate = DateTime.Now;

                _context.TblActors.Add(tblActors);
                _context.SaveChanges();


                return Json(new { code = 200, msg = "Thêm mới thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới thất bại!. Lỗi: " + ex.Message });

            }
        }


        // Chỉnh sửa diễn viên

        [HttpPost]

        public JsonResult Edit(int id, string fullName, int gender, string phone, DateTime? birthDate, string avatar, string email, int countryId, bool isActive, string address)
        {
            try
            {
                var tblActors = _context.TblActors.SingleOrDefault(x => x.ActorId == id);
                tblActors.FullName = fullName;
                tblActors.CountryId = countryId;
                tblActors.Gender = gender;
                tblActors.Phone = phone;
                tblActors.Email = email;
                tblActors.Address = address;
                tblActors.BirthDate = birthDate;
                tblActors.Avatar = avatar;

                tblActors.IsActive = isActive;

                //Lưu vào csdl
                _context.Update(tblActors);
                _context.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật diễn viên thành công" });

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật diễn viên thất bại: " + ex.Message });
            }
        }

        /// XÓA DIỄN VIÊN
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var tblActors = _context.TblActors.SingleOrDefault(x => x.ActorId == id);

                _context.Remove(tblActors);
                _context.SaveChanges();

                return Json(new { code = 200, msg = "Xóa dữ liệu thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa dữ liệu thất bại: " + ex.Message });
            }
        }

    }
}
