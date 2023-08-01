using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace VMotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // phải xác thực đăng nhập mới vào đc home

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
