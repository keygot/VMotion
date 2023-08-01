using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using VMotion.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




// Kết nối csdl

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<dbVMotionContext>(options => options.UseSqlServer(connection));


////Session
//builder.Services.AddDistributedMemoryCache();
////builder.Services.AddSession();
//builder.Services.AddSession(cfg =>
//{            // Đăng ký dịch vụ Session
//    cfg.Cookie.Name = "VMotion";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
//    cfg.IdleTimeout = new TimeSpan(0, 30, 0);   // Thời gian tồn tại của Session
//});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(p =>
               {
                   p.Cookie.Name = "UserLoginCookie";
                   p.ExpireTimeSpan = TimeSpan.FromDays(1);
                   p.LoginPath = "/adminlogin.html";
                   //p.LogoutPath = "/logout.html";
                   p.AccessDeniedPath = "/not-found.html";
               }
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Cấu hình khi tạo Area Admin
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
