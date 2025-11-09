using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelWeb.Models
{
    public static class TripPlannerService
    {
        public static TripPlan GenerateDetailedPlan(TripRequest request)
        {
            int days = request.Days ?? SuggestDaysByBudget(request.Budget);
            var budget = request.Budget;

            // Phân chia chi phí
            double foodBudget = budget * 0.25;
            double hotelBudget = budget * 0.30;
            double transportBudget = budget * 0.20;
            double funBudget = budget * 0.15;
            double otherBudget = budget * 0.10;

            var plan = new TripPlan
            {
                Destination = request.Destination,
                SuggestedDays = days,
                TotalBudget = request.Budget,
                TransportOptions = SuggestTransport(request.Destination, transportBudget),
                HotelSuggestions = SuggestHotels(request.Destination, hotelBudget),
                DailyPlans = GenerateDailyExpenses(request.Destination, days, foodBudget, funBudget)
            };

            // Tính tổng chi phí thực tế
            double total = 0;
            foreach (var d in plan.DailyPlans)
                total += d.TotalCost;
            plan.EstimatedTotalCost = total + hotelBudget + transportBudget + otherBudget;

            return plan;
        }

        private static int SuggestDaysByBudget(double budget)
        {
            if (budget < 3000000) return 2;
            if (budget < 6000000) return 3;
            if (budget < 10000000) return 4;
            return 5;
        }

        private static List<string> SuggestTransport(string dest, double budget)
        {
            return new List<string>
            {
                $"Xe giường nằm tới {dest} (khoảng {budget * 0.3:N0} VNĐ)",
                $"Máy bay (nếu xa, tầm {budget * 0.7:N0} VNĐ)"
            };
        }

        private static List<string> SuggestHotels(string dest, double budget)
        {
            return new List<string>
            {
                $"Khách sạn 3* tại trung tâm {dest} (~{budget / 3:N0} VNĐ/đêm)",
                $"Homestay hoặc Airbnb giá rẻ (~{budget / 5:N0} VNĐ/đêm)"
            };
        }

        private static List<DailyExpense> GenerateDailyExpenses(string dest, int days, double foodBudget, double funBudget)
        {
            var dailyPlans = new List<DailyExpense>();
            var random = new Random();
            var canonicalProvince = CuisineData.CanonicalProvinceName(dest);
            var cuisineTop = CuisineData.GetTopByProvince(canonicalProvince, 10);
            var destinationInfo = DestinationData.GetAll()
                .FirstOrDefault(d => string.Equals(d.Name, dest, StringComparison.OrdinalIgnoreCase)
                    || (!string.IsNullOrWhiteSpace(d.Name) && d.Name.IndexOf(dest, StringComparison.OrdinalIgnoreCase) >= 0)
                    || (!string.IsNullOrWhiteSpace(dest) && dest.IndexOf(d.Name ?? string.Empty, StringComparison.OrdinalIgnoreCase) >= 0));

            for (int day = 1; day <= days; day++)
            {
                var activities = new List<Activity>();

                // Sáng: món đặc trưng
                var breakfastIndex = (day - 1) % Math.Max(1, cuisineTop.Count);
                var breakfast = cuisineTop.Count > 0 ? cuisineTop[breakfastIndex] : null;
                activities.Add(new Activity
                {
                    Time = "Sáng",
                    Type = "Ăn uống",
                    Name = breakfast != null ? $"Ăn sáng: {breakfast.Name}" : $"Ăn sáng tại quán địa phương ở {dest}",
                    Description = breakfast != null ? $"{breakfast.Description} — Món đặc trưng {canonicalProvince}." : "Bữa sáng nhẹ nhàng với món địa phương.",
                    Cost = breakfast != null ? (double)breakfast.AveragePrice : foodBudget / (days * 3)
                });

                // Trưa: tham quan với mô tả điểm đến
                var mainPlaceName = destinationInfo?.Name ?? dest;
                var mainPlaceDesc = destinationInfo?.Description ?? $"Khám phá các điểm đến nổi bật tại {dest}.";
                activities.Add(new Activity
                {
                    Time = "Trưa",
                    Type = "Tham quan",
                    Name = $"Tham quan: {mainPlaceName}",
                    Description = mainPlaceDesc,
                    Cost = funBudget / days
                });

                // Chiều: cafe/giải trí
                activities.Add(new Activity
                {
                    Time = "Chiều",
                    Type = "Giải trí",
                    Name = "Cà phê/Ngắm hoàng hôn",
                    Description = $"Chọn quán có view đẹp tại {dest} để nghỉ ngơi và chụp ảnh.",
                    Cost = funBudget / (days * 2)
                });

                // Tối: món nổi bật khác
                var dinnerIndex = (day + 3) % Math.Max(1, cuisineTop.Count);
                var dinner = cuisineTop.Count > 0 ? cuisineTop[dinnerIndex] : null;
                activities.Add(new Activity
                {
                    Time = "Tối",
                    Type = "Ăn uống",
                    Name = dinner != null ? $"Ăn tối: {dinner.Name}" : "Ăn tối và dạo phố đêm",
                    Description = dinner != null ? $"{dinner.Description} — Giá trung bình khoảng {dinner.AveragePrice:N0} đ." : $"Thưởng thức ẩm thực đêm tại {dest}.",
                    Cost = dinner != null ? (double)dinner.AveragePrice : foodBudget / (days * 3)
                });
                dailyPlans.Add(new DailyExpense { DayNumber = day, Activities = activities });
            }

            return dailyPlans;
        }
    }
}
