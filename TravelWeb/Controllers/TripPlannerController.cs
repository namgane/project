using Microsoft.AspNetCore.Mvc;
using TravelWeb.Models;
using Microsoft.AspNetCore.Http;
using TravelWeb.Services;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.IO;
using System.Text;
using TravelWeb.Data;
using System.Security.Claims;

namespace TravelWeb.Controllers
{
    public class TripPlannerController : Controller
    {
        private Random _random = new Random();
        private readonly TravelContext _context;

        public TripPlannerController(TravelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string? destination = null, double? budget = null, int? days = null)
        {
            var model = new TripRequest
            {
                Destination = destination ?? string.Empty,
                Budget = budget ?? 0,
                Days = days
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(TripRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            // ✅ 1. Tạo plan và truyền trực tiếp lựa chọn ăn chay vào Service
            var plan = TripPlannerService.GenerateDetailedPlan(request);

            // ✅ 2. Truyền trạng thái ăn chay sang View Result qua ViewBag
            ViewBag.IsVegetarian = request.IsVegetarian;

            return View("Result", plan);
        }

        [HttpGet]
        public IActionResult Result(string destination, double budget, int? days, bool vegetarian = false)
        {
            if (string.IsNullOrWhiteSpace(destination) || budget <= 0)
            {
                TempData["ErrorMessage"] = "Thiếu thông tin để tạo kế hoạch. Vui lòng nhập lại.";
                return RedirectToAction("Index", new { destination, budget, days });
            }

            // Tạo request từ query string và đặt trạng thái ăn chay
            var req = new TripRequest
            {
                Destination = destination,
                Budget = budget,
                Days = days,
                IsVegetarian = vegetarian // ✅ Đặt trạng thái ăn chay
            };

            ViewBag.IsVegetarian = vegetarian;
            // Gọi GenerateDetailedPlan, nó sẽ tự động lọc do IsVegetarian=true/false
            var plan = TripPlannerService.GenerateDetailedPlan(req);

            return View(plan);
        }

        // =======================================================
        // ✅ ACTION AJAX ĐÃ FIX: Chỉ cần cập nhật trạng thái trong request và gọi lại Service
        // =======================================================
        [HttpPost]
        public async Task<IActionResult> ToggleVegetarian([FromBody] TripRequest request, [FromQuery] bool vegetarian)
        {
            try
            {
                // ⚠️ Cập nhật trạng thái Vegetarian trong request model trước khi gọi Service
                request.IsVegetarian = vegetarian;

                // ✅ Tạo plan, Service sẽ tự động lọc món ăn dựa trên trạng thái mới
                var plan = TripPlannerService.GenerateDetailedPlan(request);

                var renderedHtml = await RenderPartialViewToStringAsync("_DailyItineraryPartial", plan.DailyPlans);

                // ✅ Trả về JSON với null checks
                var response = new
                {
                    success = true,
                    totalDailyCost = Math.Round((plan.DailyPlans?.Sum(d => d?.TotalCost ?? 0) ?? 0)).ToString("N0"),
                    estimatedTotalCost = Math.Round(plan.EstimatedTotalCost).ToString("N0"),
                    transportTotalCost = Math.Round(plan.TransportCalculation?.TotalTransportCost ?? 0).ToString("N0"),
                    fuelCost = Math.Round(plan.TransportCalculation?.FuelCost ?? 0).ToString("N0"),
                    htmlItinerary = renderedHtml
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi server: " + ex.Message });
            }
        }

        // New endpoint: create an Order record with CreatedDate, UserId, Location, Time
        public class OrderCreateDto
        {
            public string Location { get; set; }
            public string Time { get; set; } // expects ISO or parsable datetime-local string
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto dto)
        {
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.Location) || string.IsNullOrWhiteSpace(dto.Time))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { success = false, message = "Dữ liệu đặt lịch không hợp lệ." });
                }

                if (!User.Identity?.IsAuthenticated ?? true)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new { success = false, message = "Bạn cần đăng nhập để đặt lịch." });
                }

                // Try several claim keys commonly used for user id
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                  ?? User.FindFirst("sub")?.Value
                                  ?? User.FindFirst("id")?.Value
                                  ?? User.FindFirst("userId")?.Value
                                  ?? User.FindFirst("userid")?.Value;

                if (!int.TryParse(userIdClaim, out int userId))
                {
                    // fallback: try User.Identity.Name if it contains an int id (rare)
                    if (!int.TryParse(User.Identity?.Name, out userId))
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized, new { success = false, message = "Không xác định được UserId. Vui lòng đăng nhập lại." });
                    }
                }

                if (!DateTime.TryParse(dto.Time, out DateTime requestedTime))
                {
                    if (!DateTime.TryParse(dto.Time, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out requestedTime))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new { success = false, message = "Định dạng thời gian không hợp lệ." });
                    }
                }

                var order = new Order
                {
                    CreatedDate = DateTime.Now,
                    UserId = userId,
                    Location = dto.Location,
                    Time = requestedTime
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Đặt lịch thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi server: " + ex.Message });
            }
        }

        // --- HÀM RENDER PARTIAL VIEW (Giữ nguyên) ---
        private async Task<string> RenderPartialViewToStringAsync(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.ActionName;

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewEngine = HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                var viewResult = viewEngine.FindView(ControllerContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"View '{viewName}' not found");
                }

                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}