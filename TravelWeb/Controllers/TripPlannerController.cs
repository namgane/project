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

namespace TravelWeb.Controllers
{
    public class TripPlannerController : Controller
    {
        private Random _random = new Random();

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

                // ✅ Trả về JSON với HTML đã render và chi phí vận chuyển
                var response = new
                {
                    success = true,
                    totalDailyCost = Math.Round(plan.DailyPlans.Sum(d => d.TotalCost)).ToString("N0"),
                    estimatedTotalCost = Math.Round(plan.EstimatedTotalCost).ToString("N0"),
                    transportTotalCost = Math.Round(plan.TransportCalculation.TotalTransportCost).ToString("N0"),
                    fuelCost = Math.Round(plan.TransportCalculation.FuelCost).ToString("N0"),
                    htmlItinerary = renderedHtml
                };

                return Json(response);
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