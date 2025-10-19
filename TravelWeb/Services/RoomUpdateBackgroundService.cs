using Microsoft.EntityFrameworkCore;
using TravelWeb.Data;

namespace TravelWeb.Services
{
    public class RoomUpdateBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RoomUpdateBackgroundService> _logger;

        public RoomUpdateBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<RoomUpdateBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Room Update Background Service đang chạy...");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await UpdateExpiredBookings();

                    // Chạy mỗi 1 giờ (có thể điều chỉnh)
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Lỗi khi cập nhật phòng tự động");
                    // Chờ 5 phút trước khi thử lại nếu có lỗi
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
            }
        }

        private async Task UpdateExpiredBookings()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TravelContext>();

            // Lấy tất cả booking đã xác nhận và đã đến ngày trả phòng
            var expiredBookings = await context.Bookings
                .Include(b => b.Hotel)
                .Where(b => b.TrangThai == "Đã Xác Nhận" && b.CheckOutDate.Date <= DateTime.Now.Date)
                .ToListAsync();

            if (expiredBookings.Any())
            {
                foreach (var booking in expiredBookings)
                {
                    // Cập nhật trạng thái và trả lại phòng
                    booking.TrangThai = "Đã Hoàn Thành";
                    if (booking.Hotel != null)
                    {
                        booking.Hotel.SoPhong += booking.SoLuongPhong;
                        _logger.LogInformation(
                            $"Đã hoàn thành booking #{booking.Id}, trả lại {booking.SoLuongPhong} phòng cho khách sạn {booking.Hotel.TenKhachSan}");
                    }
                }

                await context.SaveChangesAsync();
                _logger.LogInformation($"Đã cập nhật {expiredBookings.Count} booking hết hạn");
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Room Update Background Service đang dừng...");
            await base.StopAsync(stoppingToken);
        }
    }
}