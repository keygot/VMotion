using Microsoft.AspNetCore.Mvc;
using VMotion.Models;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminDirectorsController : Controller
    {
        private readonly dbVMotionContext _context;

        public AdminDirectorsController(dbVMotionContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        // Lấy danh sách bằng AJAX
        [HttpGet]
        public JsonResult DsDirectors(int trang)
        {
            try
            {
                var pageSize = int.Parse(_context.TblPhanTrangs.SingleOrDefault(x => x.TuKhoa == "so_dong_moi_trang").GiaTri);

                var dsDirectors = (from ac in _context.TblDirectors.OrderByDescending(x => x.DirectorId)
                                select new
                                {
                                    DirectorId = ac.DirectorId,
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

                var kqht = dsDirectors.Skip((trang - 1) * pageSize)
                              .Take(pageSize)
                              .ToList(); // Lưu kết quả hiện tại khi phân trang


                //// Phân trang


                var soTrang = dsDirectors.Count() % pageSize == 0 ? dsDirectors.Count() / pageSize : dsDirectors.Count() / pageSize + 1;

                return Json(new { code = 200, soTrang = soTrang, dsDirectors = kqht, msg = "Lấy danh sách thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách menu thất bại: " + ex.Message });
            }
        }

        // XEM CHI TIẾT DIỄN VIÊN

        [HttpGet]
        public JsonResult Details(int id)
        {
            try
            {
                var tblDirectors = _context.TblDirectors.FirstOrDefault(x => x.DirectorId == id);

                return Json(new { code = 200, tblDirectors = tblDirectors, msg = "Lấy thông tin đạo diễn thành công!" });
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
                var tblDirectors = new TblDirector();
                tblDirectors.FullName = fullName;
                tblDirectors.CountryId = countryId;
                tblDirectors.Gender = gender;
                tblDirectors.Phone = phone;
                tblDirectors.Email = email;
                tblDirectors.Address = address;
                tblDirectors.BirthDate = birthDate;
                tblDirectors.Avatar = avatar;

                tblDirectors.IsActive = isActive;


                tblDirectors.CreatedDate = DateTime.Now;

                _context.TblDirectors.Add(tblDirectors);
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
                var tblDirectors = _context.TblDirectors.SingleOrDefault(x => x.DirectorId == id);
                tblDirectors.FullName = fullName;
                tblDirectors.CountryId = countryId;
                tblDirectors.Gender = gender;
                tblDirectors.Phone = phone;
                tblDirectors.Email = email;
                tblDirectors.Address = address;
                tblDirectors.BirthDate = birthDate;
                tblDirectors.Avatar = avatar;

                tblDirectors.IsActive = isActive;

                //Lưu vào csdl
                _context.Update(tblDirectors);
                _context.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật thông tin thành công" });

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật thông tin thất bại: " + ex.Message });
            }
        }

        /// XÓA DIỄN VIÊN
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var tblDirectors = _context.TblDirectors.SingleOrDefault(x => x.DirectorId == id);

                _context.Remove(tblDirectors);
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
