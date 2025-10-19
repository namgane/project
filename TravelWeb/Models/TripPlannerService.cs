using System;
using System.Collections.Generic;

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

            for (int day = 1; day <= days; day++)
            {
                var activities = new List<Activity>
                {
                    new Activity { Time = "Sáng", Name = $"Ăn sáng tại quán địa phương ở {dest}", Type = "Ăn uống", Cost = foodBudget / (days * 3) },
                    new Activity { Time = "Trưa", Name = $"Tham quan điểm nổi bật ngày {day} ở {dest}", Type = "Tham quan", Cost = funBudget / days },
                    new Activity { Time = "Chiều", Name = $"Thưởng thức cà phê/ngắm hoàng hôn", Type = "Giải trí", Cost = funBudget / (days * 2) },
                    new Activity { Time = "Tối", Name = $"Ăn tối và dạo phố đêm", Type = "Ăn uống", Cost = foodBudget / (days * 3) }
                };
                dailyPlans.Add(new DailyExpense { DayNumber = day, Activities = activities });
            }

            return dailyPlans;
        }
    }
}
