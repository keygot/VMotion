using VMotion.Models;
using VMotion.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminPostsController : Controller
    {
        private readonly dbVMotionContext _context;

        public AdminPostsController(dbVMotionContext context)
        {
            _context = context;
        }


        public IActionResult Index(int page = 1)
        {

            var pageNumber = page;
            var pageSize = Functions.PAGE_SIZE;

            var lsPosts = (from post in _context.TblPosts
                           orderby post.PostId descending
                           select post).ToPagedList(pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;

            // Lấy ra danh mục

            var CategoryList = (from cat in _context.TblCategories
                                select new SelectListItem()
                                {
                                    Text = cat.CategoryName,
                                    Value = cat.CategoryId.ToString(),
                                }).ToList();
            CategoryList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn danh mục ----",
                Value = "0"
            });

            ViewBag.CategoryList = CategoryList;


            // 

            return View(lsPosts);
        }



        public IActionResult Details(int? postID)
        {
            if (postID == null || postID == 0)
            {
                return NotFound();
            }

            var tblPosts = _context.TblPosts
                           .Include(x => x.Category)
                           .FirstOrDefault(x => x.PostId == postID);

            if (tblPosts == null)
            {
                return NotFound();
            }

            return View(tblPosts);
        }


        // ============

        public IActionResult Create()
        {

            ViewData["DanhMuc"] = new SelectList(_context.TblCategories, "CategoryId", "CategoryName");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(TblPost tblPost)
        {
            if (ModelState.IsValid)
            {


                tblPost.CreatedDate = DateTime.Now;
                tblPost.Status = 3;

                _context.Add(tblPost);
                _context.SaveChanges();



                return RedirectToAction("Index");
            }
            ViewData["DanhMuc"] = new SelectList(_context.TblCategories, "CategoryId", "CategoryName", tblPost.CategoryId);

            return View(tblPost);
        }


        // ====================================

        public IActionResult Edit(int? postID)
        {
            if (postID == null || postID == 0)
            {
                return NotFound();
            }

            var tblPosts = _context.TblPosts.Find(postID);

            if (tblPosts == null)
            {
                return NotFound();

            }
            ViewData["DanhMuc"] = new SelectList(_context.TblCategories, "CategoryId", "CategoryName", tblPosts.CategoryId);

            return View(tblPosts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TblPost tblPost)
        {
            if (ModelState.IsValid)
            {


                _context.Update(tblPost);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewData["DanhMuc"] = new SelectList(_context.TblCategories, "CategoryId", "CategoryName", tblPost.CategoryId);
            return View(tblPost);
        }


        // ===================================
        public IActionResult Delete(int? postID)
        {
            if (postID == null || postID == 0)
            {
                return NotFound();
            }

            var tblPosts = _context.TblPosts
                            .Include(x => x.Category)
                            .FirstOrDefault(x => x.PostId == postID);

            if (tblPosts == null)
            {
                return NotFound();
            }

            return View(tblPosts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int postID)
        {
            var tblPosts = _context.TblPosts
                           .Where(x => x.PostId == postID)
                           .FirstOrDefault();

            if (tblPosts == null)
            {
                return NotFound();

            }

            _context.TblPosts.Remove(tblPosts);
            _context.SaveChanges();

            if (tblPosts.Thumb != null)
            {
                // Xóa ảnh 
                var fileName = "wwwroot" + tblPosts.Thumb;
                System.IO.File.Delete(fileName);
            }

            return RedirectToAction("Index");
        }


       
    }
}
