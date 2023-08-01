using Microsoft.AspNetCore.Mvc;
using VMotion.Extension;
using VMotion.Models;
using VMotion.Utilities;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccounts2Controller : Controller
    {
        private readonly dbVMotionContext _context;

        public AdminAccounts2Controller(dbVMotionContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        // Lấy danh sách bằng AJAX
        [HttpGet]
        public JsonResult DsAccount(int trang, string tuKhoa)
        {
            try
            {
                var pageSize = int.Parse(_context.TblPhanTrangs.SingleOrDefault(x => x.TuKhoa == "so_dong_moi_trang").GiaTri);

                var dsAccount = (from ac in _context.TblAccounts.OrderByDescending(x => x.AccountId)
                              join r in _context.TblRoles on ac.RoleId equals r.RoleId
                              where ac.IsActive == true
                              select new
                              {
                                  AccountId = ac.AccountId,
                                  FullName = ac.FullName,
                                  IsActive = ac.IsActive,
                                  Email = ac.Email,
                                  Phone = ac.Phone,
                                  RoleName = r.RoleName,
                                  LastLogin = ac.LastLogin.Value.ToString("dd/MM/yyyy"),


                              }).ToList();

                var kqht = dsAccount.Skip((trang - 1) * pageSize)
                              .Take(pageSize)
                              .ToList(); // Lưu kết quả hiện tại khi phân trang


                //// Phân trang


                var soTrang = dsAccount.Count() % pageSize == 0 ? dsAccount.Count() / pageSize : dsAccount.Count() / pageSize + 1;

                return Json(new { code = 200, soTrang = soTrang, dsAccount = kqht, msg = "Lấy danh sách thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách thất bại: " + ex.Message });
            }
        }



        // Hàm Json thêm mới
        [HttpPost]

        public async Task<JsonResult> Create(string fullname, int roleId, string phone, string email, bool isActive, IFormFile? fThumb)
        {
            try
            {
                var tblAccounts = new TblAccount();

                tblAccounts.IsActive = isActive;
                tblAccounts.FullName = fullname;
                tblAccounts.RoleId = roleId;
                tblAccounts.Phone = phone;
                tblAccounts.Email = email;
                tblAccounts.IsActive = isActive;
                tblAccounts.CreatedDate = DateTime.Now;

                // Tạo ngẫu nhiên mật khẩu
                string salt = Functions.GetRandomKey();

                tblAccounts.Salt = salt;
                tblAccounts.Password = (tblAccounts.Phone.Trim() + salt.Trim()).ToMD5();


                // Tạo hình ảnh
                // xử lý Thumb 

                tblAccounts.FullName = Functions.ToTitleCase(tblAccounts.FullName);


                if (fThumb != null) // nếu có chọn hình ảnh
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string imageName = Functions.SEOUrl(tblAccounts.FullName + tblAccounts.AccountId.ToString()) + extension;
                    tblAccounts.Thumb = await Functions.UploadFile(fThumb, @"account", imageName.ToLower());
                }
                if (string.IsNullOrEmpty(tblAccounts.Thumb)) tblAccounts.Thumb = "default.jpg"; // 

                _context.TblAccounts.Add(tblAccounts);
                _context.SaveChanges();


                return Json(new { code = 200, msg = "Thêm mới thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới thất bại!. Lỗi: " + ex.Message });

            }
        }


        // XEM CHI TIẾT TÀI KHOẢN

        [HttpGet]
        public JsonResult Details(int id)
        {
            try
            {
                var tblAccounts = _context.TblAccounts.FirstOrDefault(x => x.AccountId == id);

                return Json(new { code = 200, tblAccounts = tblAccounts, msg = "Lấy thông tin  thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin thất bại. Lỗi: " + ex.Message });
            }
        }


        // Chỉnh sửa tài khoản

        [HttpPost]

        public JsonResult Edit(int id, string fullname, int roleId, string phone, string email, bool isActive)
        {
            try
            {
                var tblAccounts = _context.TblAccounts.SingleOrDefault(x => x.AccountId == id);
                tblAccounts.IsActive = isActive;
                tblAccounts.FullName = fullname;
                tblAccounts.RoleId = roleId;
                tblAccounts.Phone = phone;
                tblAccounts.Email = email;
                tblAccounts.IsActive = isActive;


                
                //Lưu vào csdl
                _context.Update(tblAccounts);
                _context.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật thành công" });

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật thất bại: " + ex.Message });
            }
        }

        /// XÓA Tài Khoản
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var tblAccounts = _context.TblAccounts.SingleOrDefault(x => x.AccountId == id);

                _context.Remove(tblAccounts);
                _context.SaveChanges();

                return Json(new { code = 200, msg = "Xóa thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa thất bại: " + ex.Message });
            }
        }
    }
}
