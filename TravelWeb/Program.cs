using Microsoft.EntityFrameworkCore;
using TravelWeb.Data;
using TravelWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Thêm MVC (controllers + views)
builder.Services.AddControllersWithViews();

// Kết nối tới SQL Server
builder.Services.AddDbContext<TravelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cấu hình Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // Hết hạn sau 60 phút không hoạt động
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đăng ký Background Service để tự động cập nhật phòng khi hết hạn
builder.Services.AddHostedService<RoomUpdateBackgroundService>();

var app = builder.Build();

// Xử lý lỗi trong môi trường Production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Bắt buộc: kích hoạt Session
app.UseSession();

app.UseAuthorization();

// Cấu hình route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();