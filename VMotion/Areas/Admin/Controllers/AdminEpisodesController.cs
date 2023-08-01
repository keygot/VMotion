using Microsoft.AspNetCore.Mvc;
using VMotion.Models;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminEpisodesController : Controller
    {
        private readonly dbVMotionContext _context;
        public AdminEpisodesController(dbVMotionContext context)
        {
            _context = context;
        }
    
        public IActionResult Index()
        {
            return View();
        }
       


        // Hiển thị danh sách tập phim bằng ajax
        [HttpGet]
        public JsonResult DsEpisode(int trang)
        {
            try
            {
                var pageSize = int.Parse(_context.TblPhanTrangs.SingleOrDefault(x => x.TuKhoa == "so_dong_moi_trang").GiaTri);

                var dsEpisode = (from e in _context.TblEpisodes
                               join m in _context.TblMovies on e.MovieId equals m.MovieId
                               orderby e.EpisodeId descending
                               select new
                               {
                                   EpisodeId = e.EpisodeId,
                                   MovieName = m.MovieName,
                                   EpisodeNumber = e.EpisodeNumber,
                                   Img = e.Img,
                                   ReleaseDate = e.ReleaseDate.Value.ToString("dd/MM/yyyy"),
                                   IsActive = e.IsActive
                               }).ToList();
                var kqht = dsEpisode.Skip((trang - 1) * pageSize)
                              .Take(pageSize)
                              .ToList(); // Lưu kết quả hiện tại khi phân trang

                //// Phân trang


                var soTrang = dsEpisode.Count() % pageSize == 0 ? dsEpisode.Count() / pageSize : dsEpisode.Count() / pageSize + 1;

                return Json(new { code = 200, soTrang = soTrang, dsEpisode = kqht, msg = "Lấy danh sách thành công!" });

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách tập phim thất bại. Lỗi: " + ex.Message });
            }

            
        }



        // THÊM MỚI TẬP PHIM BẰNG AJAX
        [HttpPost]
        public JsonResult AddEpisode(int movieId, int EpisodeNumber, string Video, string Img, DateTime? ReleaseDate, bool isActive, string Description)
        {
            try
            {
                // Kiểm tra tập phim đã tồn tại hay chưa và đưa ra gợi ý
                var existingEpisode = _context.TblEpisodes.FirstOrDefault(e => e.MovieId == movieId && e.EpisodeNumber == EpisodeNumber);

                if (existingEpisode != null)
                {
                    var episodes = _context.TblEpisodes.Where(e => e.MovieId == movieId && e.EpisodeNumber != EpisodeNumber).ToList();
                    return Json(new { code = 400, msg = "Tập số đã tồn tại. Các tập chưa tồn tại: ", episodes });
                }

                var tblEpisode = new TblEpisode();

                tblEpisode.MovieId = movieId;
                tblEpisode.EpisodeNumber = EpisodeNumber;
                tblEpisode.Video = Video;
                tblEpisode.Img = Img;
                tblEpisode.ReleaseDate = ReleaseDate;
                tblEpisode.Description = Description;
                tblEpisode.IsActive = isActive;
                tblEpisode.CreatedDate = DateTime.Now;

                _context.TblEpisodes.Add(tblEpisode);
                _context.SaveChanges();

                return Json(new { code = 200, msg = "Thêm mới tập phim thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới tập phim thất bại. Lỗi: " + ex.Message });
            }
        }




    }
}
