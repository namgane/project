using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using TravelWeb.Data;

namespace TravelWeb.Models
{
    // Class chứa thông tin gợi ý thay thế
    public class Suggestion
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public static class TripPlannerService
    {
        private static Random _random = new Random();

        public static TripPlan GenerateDetailedPlan(TripRequest request, TravelContext dbContext)
        {
            var dbLocations = dbContext?.Locations?.ToList() ?? new List<Location>();
            return GenerateDetailedPlan(request, dbLocations);
        }

        public static TripPlan GenerateDetailedPlan(TripRequest request, List<Location>? dbLocations = null)
        {
            // Reset Random
            _random = new Random(Guid.NewGuid().GetHashCode());

            int days = request.Days ?? SuggestDaysByBudget(request.Budget);
            var budget = request.Budget;
            int people = Math.Max(1, request.NumberOfPeople);
            string transportType = request.TransportType ?? "Kết hợp tự động";

            double foodBudgetPerPerson = (budget * 0.24) / people;
            double hotelBudget = budget * 0.28;
            double funBudgetPerPerson = (budget * 0.15) / people;
            double otherBudget = budget * 0.10;

            // 1. Tạo lịch trình chi tiết (có giá sơ bộ)
            var dailyPlansResult = GenerateDailyExpenses(
                request.Destination, days, foodBudgetPerPerson, funBudgetPerPerson, 0,
                transportType, people, request.IsVegetarian, dbLocations
            );

            // 2. Tính tổng quãng đường thực tế (Dùng Regex để fix lỗi 0 km)
            double totalActualDistance = 0;
            Regex numberRegex = new Regex(@"[0-9]+([.,][0-9]+)?");

            foreach (var day in dailyPlansResult)
            {
                foreach (var activity in day.Activities)
                {
                    if (activity.Type == "Di chuyển" && !string.IsNullOrEmpty(activity.Address) && activity.Address.Contains("km"))
                    {
                        var match = numberRegex.Match(activity.Address);
                        if (match.Success)
                        {
                            string numberStr = match.Value.Replace(',', '.');
                            if (double.TryParse(numberStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double km))
                            {
                                totalActualDistance += km;
                            }
                        }
                    }
                }
            }

            // 3. Tính toán chi phí vận chuyển & Số lượng xe
            // Hàm này đã được sửa để khớp với Model mới của bạn (TotalDistance, VehiclesNeeded)
            var transportCalc = CalculateDetailedTransportCost(transportType, days, people, totalActualDistance);

            // 4. Điều chỉnh giá thực tế
            double totalTransportCostInItinerary = dailyPlansResult
                .SelectMany(d => d.Activities)
                .Where(a => a.Type == "Di chuyển")
                .Sum(a => a.Cost);

            if (totalTransportCostInItinerary > 0 && transportCalc.TotalTransportCost > 0
                && transportType != "Xe riêng" && transportType != "Xe máy")
            {
                double adjustmentFactor = transportCalc.TotalTransportCost / totalTransportCostInItinerary;
                if (adjustmentFactor > 0.1)
                {
                    foreach (var day in dailyPlansResult)
                        foreach (var activity in day.Activities)
                            if (activity.Type == "Di chuyển") activity.Cost *= adjustmentFactor;
                }
            }

            // 5. Tạo Object TripPlan hoàn chỉnh
            var plan = new TripPlan
            {
                Destination = request.Destination,
                SuggestedDays = days,
                NumberOfPeople = people,
                TotalBudget = request.Budget,
                TransportOptions = SuggestTransport(request.Destination, transportCalc.TotalTransportCost),
                HotelSuggestions = SuggestHotels(request.Destination, hotelBudget, people),
                DailyPlans = dailyPlansResult,
                TransportCalculation = transportCalc
            };

            // 6. Cân đối ngân sách cuối cùng
            double totalDailyCost = plan.DailyPlans.Sum(d => d.TotalCost);
            double estimatedTotal = totalDailyCost + hotelBudget + otherBudget + transportCalc.TotalTransportCost;

            if (estimatedTotal > budget)
            {
                double scaleFactor = (budget * 0.95) / estimatedTotal;
                foreach (var dailyPlan in plan.DailyPlans)
                    foreach (var activity in dailyPlan.Activities)
                        if (activity.Type == "Ăn uống" || activity.Type == "Giải trí") activity.Cost *= scaleFactor;

                estimatedTotal = plan.DailyPlans.Sum(d => d.TotalCost) + plan.TransportCalculation.TotalTransportCost + hotelBudget + otherBudget;
            }

            plan.EstimatedTotalCost = Math.Min(estimatedTotal, budget * 0.98);

            return plan;
        }

        private static List<DailyExpense> GenerateDailyExpenses(
            string dest, int days, double foodBudgetPerPerson, double funBudgetPerPerson,
            double transportBudget, string transportType, int people, bool isVegetarian,
            List<Location>? dbLocations = null)
        {
            var dailyPlans = new List<DailyExpense>();
            var canonicalProvince = CuisineData.CanonicalProvinceName(dest);
            var cuisineTop = CuisineData.GetTopByProvince(canonicalProvince, 20, isVegetarian).OrderBy(x => _random.Next()).ToList();

            List<AttractionPoint> attractions;
            if (dbLocations != null && dbLocations.Count > 0)
            {
                var matched = dbLocations.Where(l => l.Ten.Contains(dest) || dest.Contains(l.Ten)).ToList();
                attractions = (matched.Any() ? matched : dbLocations)
                    .Select(l => new AttractionPoint { Name = l.Ten, Description = l.MoTa, Latitude = l.Lat, Longitude = l.Lng, Type = "Local", VisitDuration = 60 })
                    .OrderBy(x => _random.Next()).ToList();
            }
            else
            {
                var destinationInfo = DestinationData.GetAll().FirstOrDefault(d => d.Name.Contains(dest) || dest.Contains(d.Name));
                attractions = destinationInfo?.Attractions?.OrderBy(x => _random.Next()).ToList() ?? new List<AttractionPoint>();
            }

            double[] baseDistances = { 6.5, 7.2, 7.8, 7.3, 6.8 };
            int numVehicles = transportType == "Xe bus" ? 1 : (transportType == "Taxi" || transportType == "Kết hợp tự động" ? (int)Math.Ceiling(people / 4.0) : (int)Math.Ceiling(people / 2.0));
            double costPerKm = (transportType == "Xe bus") ? 7000 / 8.0 : (transportType == "Taxi" || transportType == "Kết hợp tự động" ? 11000 : (2.5 / 100) * 23000);

            var usedCuisineIndices = new HashSet<int>();
            var usedAttractionIndices = new HashSet<int>();

            for (int day = 1; day <= days; day++)
            {
                var activities = new List<Activity>();
                double[] segmentDistances = baseDistances.Select(d => d * (0.85 + _random.NextDouble() * 0.3)).ToArray();

                var breakfast = GetRandomUnusedCuisineItem(cuisineTop, usedCuisineIndices);
                var lunch = GetRandomUnusedCuisineItem(cuisineTop, usedCuisineIndices);
                var dinner = GetRandomUnusedCuisineItem(cuisineTop, usedCuisineIndices);
                var attraction = GetRandomUnusedAttraction(attractions, usedAttractionIndices);

                // --- SÁNG ---
                if (breakfast != null)
                {
                    activities.Add(new Activity
                    {
                        Time = "07:00 - 08:30",
                        Type = "Ăn uống",
                        Name = $"Ăn sáng:",
                        Description = breakfast.Description,
                        Address = AddressData.GetBreakfastAddress(dest, breakfast.Name),
                        Cost = (double)breakfast.AveragePrice * people * RandomVariation(),
                        Suggestions = GetUniqueSuggestions(cuisineTop, breakfast.Name, x => x.Name, x => AddressData.GetBreakfastAddress(dest, x.Name))
                    });
                }
                else activities.Add(new Activity { Time = "07:00", Type = "Ăn uống", Name = "Ăn sáng tự túc", Address = dest, Cost = 50000 * people });

                activities.Add(new Activity { Time = "08:45", Type = "Di chuyển", Name = "Di chuyển tham quan", Address = $"- {segmentDistances[0]:F1} km", Cost = segmentDistances[0] * costPerKm * numVehicles });

                // --- THAM QUAN ---
                if (attraction != null)
                {
                    activities.Add(new Activity
                    {
                        Time = "09:30 - 12:30",
                        Type = "Tham quan",
                        Name = $"Tham quan: {attraction.Name}",
                        Description = attraction.Description,
                        Address = $"📍 {attraction.Name}, {dest}",
                        Cost = 50000 * people * RandomVariation(),
                        Suggestions = GetUniqueSuggestions(attractions, attraction.Name, x => x.Name, x => $"📍 {x.Name}, {dest}")
                    });
                }
                else activities.Add(new Activity { Time = "09:30", Type = "Tham quan", Name = $"Tham quan {dest}", Address = dest, Cost = 0 });

                activities.Add(new Activity { Time = "12:45", Type = "Di chuyển", Name = "Di chuyển ăn trưa", Address = $"- {segmentDistances[1]:F1} km", Cost = segmentDistances[1] * costPerKm * numVehicles });

                // --- TRƯA ---
                if (lunch != null)
                {
                    activities.Add(new Activity
                    {
                        Time = "13:00 - 14:30",
                        Type = "Ăn uống",
                        Name = $"Ăn trưa:",
                        Description = lunch.Description,
                        Address = AddressData.GetRestaurantAddress(dest, lunch.Name),
                        Cost = (double)lunch.AveragePrice * people * RandomVariation(),
                        Suggestions = GetUniqueSuggestions(cuisineTop, lunch.Name, x => x.Name, x => AddressData.GetRestaurantAddress(dest, x.Name))
                    });
                }

                activities.Add(new Activity { Time = "14:45", Type = "Di chuyển", Name = "Di chuyển cafe", Address = $"- {segmentDistances[2]:F1} km", Cost = segmentDistances[2] * costPerKm * numVehicles });
                activities.Add(new Activity { Time = "15:00", Type = "Giải trí", Name = "Cafe chiều", Description = "Thư giãn", Address = AddressData.GetCafeAddress(dest), Cost = 100000 * people });
                activities.Add(new Activity { Time = "17:45", Type = "Di chuyển", Name = "Về trung tâm", Address = $"- {segmentDistances[3]:F1} km", Cost = segmentDistances[3] * costPerKm * numVehicles });

                // --- TỐI ---
                if (dinner != null)
                {
                    activities.Add(new Activity
                    {
                        Time = "18:30 - 20:00",
                        Type = "Ăn uống",
                        Name = $"Ăn tối:",
                        Description = dinner.Description,
                        Address = AddressData.GetRestaurantAddress(dest, dinner.Name),
                        Cost = (double)dinner.AveragePrice * people * RandomVariation(),
                        Suggestions = GetUniqueSuggestions(cuisineTop, dinner.Name, x => x.Name, x => AddressData.GetRestaurantAddress(dest, x.Name))
                    });
                }

                activities.Add(new Activity { Time = "20:15", Type = "Di chuyển", Name = "Về khách sạn", Address = $"- {segmentDistances[4]:F1} km", Cost = segmentDistances[4] * costPerKm * numVehicles });
                dailyPlans.Add(new DailyExpense { DayNumber = day, Activities = activities });
            }
            return dailyPlans;
        }

        // Helper Methods
        private static List<Suggestion> GetUniqueSuggestions<T>(List<T> list, string currentName, Func<T, string> getName, Func<T, string> getAddress)
        {
            if (list == null) return new List<Suggestion>();
            return list.Where(x => getName(x) != currentName).OrderBy(x => Guid.NewGuid()).Take(3)
                .Select(x => new Suggestion { Name = getName(x), Address = getAddress(x) }).ToList();
        }

        private static CuisineItem GetRandomUnusedCuisineItem(List<CuisineItem> list, HashSet<int> used)
        {
            if (list == null || !list.Any()) return null;
            var available = Enumerable.Range(0, list.Count).Where(i => !used.Contains(i)).ToList();
            if (!available.Any()) { used.Clear(); available = Enumerable.Range(0, list.Count).ToList(); }
            int idx = available[_random.Next(available.Count)]; used.Add(idx); return list[idx];
        }

        private static AttractionPoint GetRandomUnusedAttraction(List<AttractionPoint> list, HashSet<int> used)
        {
            if (list == null || !list.Any()) return null;
            var available = Enumerable.Range(0, list.Count).Where(i => !used.Contains(i)).ToList();
            if (!available.Any()) { used.Clear(); available = Enumerable.Range(0, list.Count).ToList(); }
            int idx = available[_random.Next(available.Count)]; used.Add(idx); return list[idx];
        }

        private static int SuggestDaysByBudget(double b) => b < 3000000 ? 2 : (b < 6000000 ? 3 : (b < 10000000 ? 4 : 5));
        private static double RandomVariation() => 0.85 + (_random.NextDouble() * 0.30);
        private static (string, string) GetTransportInfo(string t) => t switch { "Xe riêng" => ("🏍️", "Xe máy riêng"), "Xe bus" => ("🚌", "Xe bus"), "Xe máy" => ("🏍️", "Xe máy thuê"), _ => ("🚕", "Taxi/Grab") };

        // --- CẬP NHẬT HÀM NÀY ĐỂ KHỚP VỚI MODEL MỚI ---
        private static TransportCalculation CalculateDetailedTransportCost(string type, int days, int people, double dist)
        {
            double cost = 0;
            int vehiclesNeeded = 0; // Đổi tên biến cho khớp ý nghĩa

            if (type == "Xe bus")
            {
                vehiclesNeeded = 0;
                cost = 7000 * 5 * days * people;
            }
            else if (type == "Xe máy" || type == "Xe riêng")
            {
                vehiclesNeeded = (int)Math.Ceiling(people / 2.0);
                // Nếu xe máy thuê thì tính thêm tiền thuê, xe riêng chỉ tính xăng
                cost = (dist / 100) * 2.5 * 23000 * vehiclesNeeded + (type == "Xe máy" ? 100000 * days * vehiclesNeeded : 0);
            }
            else
            {
                // Taxi/Grab
                vehiclesNeeded = (int)Math.Ceiling(people / 4.0);
                cost = dist * 11000 * vehiclesNeeded;
            }

            // Trả về Object theo Model mới của bạn
            return new TransportCalculation
            {
                TransportType = type,
                NumberOfPeople = people,      // Đã có trong Model mới
                VehiclesNeeded = vehiclesNeeded, // Đã đổi tên từ VehicleCount -> VehiclesNeeded
                TotalDistance = dist,         // Đã đổi tên từ Distance -> TotalDistance
                TotalTransportCost = cost,
                TotalDuration = dist * 2,     // Ước tính sơ bộ (2 phút/km)
                FuelCost = (type != "Xe bus") ? (dist / 100) * 2.5 * 23000 * vehiclesNeeded : 0, // Tính sơ bộ xăng
                Segments = new List<RouteSegment>() // Khởi tạo list rỗng để tránh null reference
            };
        }

        private static List<string> SuggestTransport(string d, double b) => new List<string> { $"Xe khách tới {d}", "Máy bay" };
        private static List<string> SuggestHotels(string d, double b, int p) => new List<string> { $"Khách sạn tại {d}", "Homestay" };
    }
}