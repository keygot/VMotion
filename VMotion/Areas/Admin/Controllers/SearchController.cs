using VMotion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly dbVMotionContext _context;

        public SearchController(dbVMotionContext context) {
            _context = context;
        }



        // Tìm kiếm danh mục
        public IActionResult FindCategory(string keyword)
        {
            List<TblCategory> ls = new List<TblCategory>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                var list = _context.TblCategories.AsNoTracking()
                                  .OrderByDescending(x => x.CategoryName)
                                  .Take(10)
                                  .ToList();
                return PartialView("ListCategoriesSearchPartial", list);
            }


            ls = _context.TblCategories.AsNoTracking()
                                  .Where(x => x.CategoryName.Contains(keyword))
                                  .OrderByDescending(x => x.CategoryName)
                                  .Take(10)
                                  .ToList();
            if (ls == null)
            {
                return PartialView("ListCategoriesSearchPartial", null);
            }
            else
            {
                return PartialView("ListCategoriesSearchPartial", ls);
            }
        }






        //// Tìm kiếm menu
        public IActionResult FindMenus(string keyword)
        {
            List<TblMenu> ls = new List<TblMenu>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                var list = _context.TblMenus.AsNoTracking()
                                  .OrderByDescending(x => x.MenuName)
                                  .Take(10)
                                  .ToList();
                return PartialView("ListMenusSearchPartial", list);
            }


            ls = _context.TblMenus.AsNoTracking()
                                  .Where(x => x.MenuName.Contains(keyword))
                                  .OrderByDescending(x => x.MenuName)
                                  .Take(10)
                                  .ToList();
            if (ls == null)
            {
                return PartialView("ListMenusSearchPartial", null);
            }
            else
            {
                return PartialView("ListMenusSearchPartial", ls);
            }
        }

        // Tìm kiếm tài khoản
        public IActionResult FindAccounts(string keyword)
        {
            List<TblAccount> ls = new List<TblAccount>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                var list = _context.TblAccounts.AsNoTracking()
                                  .Include(x => x.Role)
                                  .OrderByDescending(x => x.FullName)
                                  .Take(10)
                                  .ToList();
                return PartialView("ListAccountsSearchPartial", list);
            }


            ls = _context.TblAccounts.AsNoTracking()
                                  .Include(x => x.Role)
                                  .Where(x => x.FullName.Contains(keyword))
                                  .OrderByDescending(x => x.FullName)
                                  .Take(10)
                                  .ToList();
            if (ls == null)
            {
                return PartialView("ListAccountsSearchPartial", null);
            }
            else
            {
                return PartialView("ListAccountsSearchPartial", ls);
            }
        }

        // Tìm kiếm diễn viên
        public IActionResult FindActors(string keyword)
        {
            List<TblActor> ls = new List<TblActor>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                var list = _context.TblActors.AsNoTracking()
                                  .Include(x => x.Country)
                                  .OrderByDescending(x => x.FullName)
                                  .Take(10)
                                  .ToList();
                return PartialView("ListActorsSearchPartial", list);
            }


            ls = _context.TblActors.AsNoTracking()
                                  .Where(x => x.FullName.Contains(keyword))
                                  .OrderByDescending(x => x.FullName)
                                  .Take(10)
                                  .ToList();
            if (ls == null)
            {
                return PartialView("ListActorsSearchPartial", null);
            }
            else
            {
                return PartialView("ListActorsSearchPartial", ls);
            }
        }

        // Tìm kiếm quyền
        public IActionResult FindRoles(string keyword)
        {
            List<TblRole> ls = new List<TblRole>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                var list = _context.TblRoles.AsNoTracking()
                                  .OrderByDescending(x => x.RoleName)
                                  .Take(10)
                                  .ToList();
                return PartialView("ListRolesSearchPartial", list);
            }


            ls = _context.TblRoles.AsNoTracking()
                                  .Where(x => x.RoleName.Contains(keyword))
                                  .OrderByDescending(x => x.RoleName)
                                  .Take(10)
                                  .ToList();
            if (ls == null)
            {
                return PartialView("ListRolesSearchPartial", null);
            }
            else
            {
                return PartialView("ListRolesSearchPartial", ls);
            }
        }


        //// Tìm kiếm tin tức
        //public IActionResult FindPosts(string keyword)
        //{
        //    List<TblPost> ls = new List<TblPost>();
        //    if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
        //    {
        //        var list = _context.TblPosts.AsNoTracking()
        //                          .Include(x => x.Category)
        //                          .OrderByDescending(x => x.Title)
        //                          .Take(10)
        //                          .ToList();

        //        return PartialView("ListPostsSearchPartial", list);
        //    }


        //    ls = _context.TblPosts.AsNoTracking()
        //                          .Include(x => x.Category)
        //                          .Where(x => x.Title.Contains(keyword))
        //                          .OrderByDescending(x => x.Title)
        //                          .Take(10)
        //                          .ToList();
        //    if (ls == null)
        //    {
        //        return PartialView("ListPostsSearchPartial", null);
        //    }
        //    else
        //    {
        //        return PartialView("ListPostsSearchPartial", ls);
        //    }
        //}
    }
}
