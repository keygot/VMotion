using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PagedList.Core;
using System.IO;
using VMotion.Extension;
using VMotion.Models;
using VMotion.Utilities;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminMoviesController : Controller
    {
        private readonly dbVMotionContext _context;

        public AdminMoviesController(dbVMotionContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1, int ct = 0)
        {
            var pageNumber = page;
            var pageSize = Functions.PAGE_SIZE;

            List<TblMovie> lsMovies = new List<TblMovie>();

            if (ct != 0)
            {
                lsMovies = (from ac in _context.TblMovies
                            join rl in _context.TblCategories on ac.CategoryId equals rl.CategoryId
                            where ac.CategoryId == ct
                            orderby ac.MovieId descending
                            select ac).ToList();
            }
            else
            {
                lsMovies = (from ac in _context.TblMovies
                            join rl in _context.TblCategories on ac.CategoryId equals rl.CategoryId
                            orderby ac.MovieId descending
                            select ac).ToList();
            }

            PagedList<TblMovie> lsModels = new PagedList<TblMovie>(lsMovies.AsQueryable(), pageNumber, pageSize);

            ViewBag.CategoryList = ct;
            ViewBag.CurrentPage = pageNumber;

            ViewData["CategoryList"] = new SelectList(_context.TblCategories, "CategoryId", "CategoryName");

            return View(lsModels);
        }


        // Lấy danh sách moive dùng cho thẻ select 

        public JsonResult AllMovie()
        {
            try
            {
                var allMovie = (from mv in _context.TblMovies.Where(x => x.IsActive == true)
                                select new
                                {
                                    MovieId = mv.MovieId,
                                    MovieName = mv.MovieName
                                }).ToList();

                return Json(new { code = 200, allMovie = allMovie, msg = "Lấy danh sách phim thành công!" });
            }

            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách menu thất bại. Lỗi " + ex.Message });
            }
        }


        // Hiển thị danh sách phim bằng ajax
        // Hiển thị danh sách phim bằng ajax
        [HttpGet]
        public JsonResult DsMovie()
        {
            try
            {
                var dsMovie = (from m in _context.TblMovies
                               join c in _context.TblCategories on m.CategoryId equals c.CategoryId
                               orderby m.MovieId descending
                               select new
                               {
                                   MovieId = m.MovieId,
                                   MovieName = m.MovieName,
                                   CategoryName = c.CategoryName,
                                   ReleaseDate = m.ReleaseDate.Value.ToString("dd/MM/yyyy"),

                                   IsActive = m.IsActive
                               }).ToList();

                return Json(new { code = 200, dsMovie = dsMovie, msg = "Lấy danh sách phim thành công!" });

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách phim thất bại. Lỗi: " + ex.Message });
            }
        }


        // XEM CHI TIẾT PHIM 

        
        public IActionResult Details(int? id)
        {


            if (id == null || id == 0)
            {
                return NotFound();
            }
            // Lấy danh sách diễn viên
            var ActorList = (from role in _context.TblActors
                             where role.IsActive == true
                             select new SelectListItem()
                             {
                                 Text = role.FullName,
                                 Value = role.ActorId.ToString(),
                             }).ToList();
            ActorList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn tất cả ----",
                Value = "0"
            });
            // Lấy danh sách đạo diễn
            var DirectorList = (from role in _context.TblDirectors
                                where role.IsActive == true
                                select new SelectListItem()
                                {
                                    Text = role.FullName,
                                    Value = role.DirectorId.ToString(),
                                }).ToList();
            DirectorList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn tất cả ----",
                Value = "0"
            });

            // Lấy danh sách danh mục
            var CategoryList = (from role in _context.TblCategories
                                where role.IsActive == true
                                select new SelectListItem()
                                {
                                    Text = role.CategoryName,
                                    Value = role.CategoryId.ToString(),
                                }).ToList();
            CategoryList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn danh mục ----",
                Value = "0"
            });

            // Lấy danh sách thể loại
            var GenreList = (from role in _context.TblGenres
                             select new SelectListItem()
                             {
                                 Text = role.GenreName,
                                 Value = role.GenreId.ToString(),
                             }).ToList();
            GenreList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn tất cả ----",
                Value = "0"
            });

            ViewBag.GenreList = GenreList;
            ViewBag.ActorList = ActorList;
            ViewBag.DirectorList = DirectorList;
            ViewBag.CategoryList = CategoryList;

            var tblMovies = _context.TblMovies
                            .Include(x => x.Category)
                            .FirstOrDefault(x => x.MovieId == id);

            if (tblMovies == null)
            {
                return NotFound();
            }

            return View(tblMovies);
        }


        public IActionResult Create()
        {
            // Lấy danh sách diễn viên
            var ActorList = (from role in _context.TblActors
                             where role.IsActive == true
                             select new SelectListItem()
                             {
                                 Text = role.FullName,
                                 Value = role.ActorId.ToString(),
                             }).ToList();
            ActorList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn tất cả ----",
                Value = "0"
            });

            // Lấy danh sách đạo diễn
            var DirectorList = (from role in _context.TblDirectors
                                where role.IsActive == true
                                select new SelectListItem()
                                {
                                    Text = role.FullName,
                                    Value = role.DirectorId.ToString(),
                                }).ToList();
            DirectorList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn tất cả ----",
                Value = "0"
            });

            // Lấy danh sách danh mục
            var CategoryList = (from role in _context.TblCategories
                                where role.IsActive == true
                                select new SelectListItem()
                                {
                                    Text = role.CategoryName,
                                    Value = role.CategoryId.ToString(),
                                }).ToList();
            CategoryList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn danh mục ----",
                Value = "0"
            });

            // Lấy danh sách thể loại
            var GenreList = (from role in _context.TblGenres
                             select new SelectListItem()
                             {
                                 Text = role.GenreName,
                                 Value = role.GenreId.ToString(),
                             }).ToList();
            GenreList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn tất cả ----",
                Value = "0"
            });

            ViewBag.GenreList = GenreList;
            ViewBag.ActorList = ActorList;
            ViewBag.DirectorList = DirectorList;
            ViewBag.CategoryList = CategoryList;

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(TblMovie model, IFormCollection genres, IFormCollection actors, IFormCollection directors)
        {
            // Lưu thông tin phim vào bảng TblMovie
            _context.TblMovies.Add(model);
            await _context.SaveChangesAsync();

            // Giữ lại giá trị MovieId
            int movieId = model.MovieId;


            // Lưu thông tin thể loại vào bảng trung gian TblMovieGenres
            foreach (string genreId in genres["Genres"])
            {
                TblMovieGenre movieGenre = new TblMovieGenre()
                {
                    MovieId = model.MovieId,
                    GenreId = int.Parse(genreId)
                };
                _context.TblMovieGenres.Add(movieGenre);
            }
            await _context.SaveChangesAsync();


            // Lưu thông tin diễn viên vào bảng trung gian TblActorMovie
            foreach (string actorId in actors["Actors"])
            {
                TblActorMovie movieActor = new TblActorMovie()
                {
                    MovieId = movieId,
                    ActorId = int.Parse(actorId)
                };
                _context.TblActorMovies.Add(movieActor);
            }
            await _context.SaveChangesAsync();

            // Lưu thông tin đạo diễn vào bảng trung gian TblDirectorMovie
            foreach (string directorId in directors["Directors"])
            {
                TblDirectorMovie movieDirector = new TblDirectorMovie()
                {
                    MovieId = model.MovieId,
                    DirectorId = int.Parse(directorId)
                };
                _context.TblDirectorMovies.Add(movieDirector);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }




        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var tblMovies = _context.TblMovies
                        .Include(x => x.Category)
                        .FirstOrDefault(x => x.MovieId == id);

            if (tblMovies == null)
            {
                return NotFound();
            }

            // Lấy danh sách diễn viên
            var ActorList = (from role in _context.TblActors
                             where role.IsActive == true
                             select new SelectListItem()
                             {
                                 Text = role.FullName,
                                 Value = role.ActorId.ToString(),
                             }).ToList();
            ActorList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn tất cả ----",
                Value = "0"
            });

            // Lấy danh sách đạo diễn
            var DirectorList = (from role in _context.TblDirectors
                                where role.IsActive == true
                                select new SelectListItem()
                                {
                                    Text = role.FullName,
                                    Value = role.DirectorId.ToString(),
                                }).ToList();
            DirectorList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn tất cả ----",
                Value = "0"
            });

            // Lấy danh sách danh mục
            var CategoryList = (from role in _context.TblCategories
                                where role.IsActive == true
                                select new SelectListItem()
                                {
                                    Text = role.CategoryName,
                                    Value = role.CategoryId.ToString(),
                                }).ToList();
            CategoryList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn danh mục ----",
                Value = "0"
            });

            // Lấy danh sách thể loại
            var GenreList = (from role in _context.TblGenres
                             select new SelectListItem()
                             {
                                 Text = role.GenreName,
                                 Value = role.GenreId.ToString(),
                             }).ToList();
            GenreList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn tất cả ----",
                Value = "0"
            });

            ViewBag.GenreList = GenreList;
            ViewBag.ActorList = ActorList;
            ViewBag.DirectorList = DirectorList;
            ViewBag.CategoryList = CategoryList;

            return View(tblMovies);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TblMovie model, IFormCollection genres, IFormCollection actors, IFormCollection directors)
        {
            // Lưu thông tin phim vào bảng TblMovie
            model.ModifiedDate = DateTime.Now;
            _context.TblMovies.Update(model);
            await _context.SaveChangesAsync();

            

            if (genres.Count > 0)
            {
                // Lưu thông tin thể loại mới vào bảng trung gian TblMovieGenres
                foreach (string genreId in genres["Genres"])
                {
                    int genreIdInt = int.Parse(genreId);
                    // Kiểm tra xem thể loại đã tồn tại trong bảng trung gian TblMovieGenres hay chưa
                    if (!_context.TblMovieGenres.Any(x => x.MovieId == model.MovieId && x.GenreId == genreIdInt))
                    {
                        TblMovieGenre movieGenre = new TblMovieGenre()
                        {
                            MovieId = model.MovieId,
                            GenreId = genreIdInt
                        };
                        _context.TblMovieGenres.Add(movieGenre);
                    }

                }
            }


            if(actors.Count > 0)
            {
                // Lưu thông tin diễn viên mới vào bảng trung gian TblActorMovie
                foreach (string actorId in actors["Actors"])
                {
                    int actorIdInt = int.Parse(actorId);
                    // Kiểm tra xem diễn viên đã tồn tại trong bảng trung gian TblActorMovie hay chưa
                    if (!_context.TblActorMovies.Any(x => x.MovieId == model.MovieId && x.ActorId == actorIdInt))
                    {
                        TblActorMovie movieActor = new TblActorMovie()
                        {
                            MovieId = model.MovieId,
                            ActorId = actorIdInt
                        };
                        _context.TblActorMovies.Add(movieActor);
                    }
                }

            }

            
            if(directors.Count > 0)
            {
                // Lưu thông tin đạo diễn mới vào bảng trung gian TblDirectorMovie
                foreach (string directorId in directors["Directors"])
                {
                    int directorIdInt = int.Parse(directorId);
                    // Kiểm tra xem đạo diễn đã tồn tại trong bảng trung gian TblDirectorMovie hay chưa
                    if (!_context.TblDirectorMovies.Any(x => x.MovieId == model.MovieId && x.DirectorId == directorIdInt))
                    {
                        TblDirectorMovie movieDirector = new TblDirectorMovie()
                        {
                            MovieId = model.MovieId,
                            DirectorId = directorIdInt
                        };
                        _context.TblDirectorMovies.Add(movieDirector);
                    }

                }
            }

            

            // Lưu thông tin phim vào database
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");

        }



        


        // XÓA PHIM 
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var tblMovies = _context.TblMovies.SingleOrDefault(x => x.MovieId == id);
                tblMovies.IsActive = false;

                _context.SaveChanges();

                return Json(new { code = 200, msg = "Xóa phim thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa phim thất bại: " + ex.Message });
            }
        }

    }
}
